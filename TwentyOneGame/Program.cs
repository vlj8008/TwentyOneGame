using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Casino;
using Casino.TwentyOne;
using System.Data.SqlClient;
using TwentyOneGame;

namespace TwentyOne
{
    class Program
    {
        static void Main(string[] args)
        {


            //Simple example of polymorphism the Twenty one game object morphed into 
            //the game object, which is a higher order object. TwentyOne game inherits 
            //from game class that is why it can morph into game Class. This can be useful 
            //for example if we have to list all the games. A list can only take one data 
            //type so the twentyOne game would have to become (morph) a "game" datatype.

            //List<Game> games = new List<Game>();
            //Game game = new TwentyOneGame();
            //games.Add(game);


            //Player newPlayer = new Player("Vicky "); Using a contructor call chain. Output: Name: Vicky Balance: 100

            const string casinoName = "Royal Casino";

            Console.WriteLine("Welcome to the {0}. Let's start by getting your name?", casinoName);
            string playerName = Console.ReadLine();
            Console.WriteLine("Hello  {0}", playerName);

            if (playerName.ToLower() == "admin")
            {
                List<ExceptionEntity> Exceptions = ReadExceptions();

                foreach (var exception in Exceptions)
                {
                    Console.Write(exception.Id + " | ");
                    Console.Write(exception.ExceptionType + " | ");
                    Console.Write(exception.ExceptionMessage + " | ");
                    Console.Write(exception.TimeStamp + " | ");

                }
                Console.Read();
                return;

            }

            //*Exception handling:

            bool validAnswer = false;
            int bank = 0;
            while (!validAnswer)
            {
                Console.WriteLine("And how much money did you bring today?");

                //TryParse is method that takes in a string(Console.ReadLine) value of a number, converts it to integer
                //returns true if conversion successful, other wise false.

                validAnswer = int.TryParse(Console.ReadLine(), out bank);

                if (validAnswer == false)
                {
                    Console.WriteLine("Please enter a whole number only. No decimals");
                }

            }

            Console.WriteLine("Would you like to play a game of Twenty One right now?");
            string answer = Console.ReadLine().ToLower();

            if (answer == "yes" || answer == "y" || answer == "ya" || answer == "yeah")
            {
                Console.WriteLine("Great");

                Player player = new Player(playerName, bank);
                player.Id = Guid.NewGuid();

                using (StreamWriter file = new StreamWriter(@"C:\Users\vicky\OneDrive\Documents\GitHub\Basic_C#_Programs\log.txt", true))
                {
                    file.WriteLine(player.Id);

                }

                Game game = new TwentyOneGame();//polymorphism so can access overloaded operator

                game = game + player;

                player.isActivelyPlaying = true;

                while (player.isActivelyPlaying && bank > 0)
                {
                    try
                    {
                        game.Play();
                    }
                    catch (FraudException ex) //start with more specific exceptions. 
                    {
                        Console.WriteLine(ex.Message);
                        UpdateDbWithException(ex);
                        Console.ReadLine();
                        return; //when type return in void method, it ends the method. Handy trick.
                    }

                    catch (Exception ex) //have general exceptions below specific one. 
                    {
                        Console.WriteLine("An error occured. Please contact your System Administrator.");
                        UpdateDbWithException(ex);
                        Console.ReadLine();
                        return;
                    }

                }

                game = game - player;
                Console.WriteLine("Thanks for playing.");

            }

            Console.WriteLine("Feel free to look around the casino. Bye for now");
            Console.ReadLine();

        }

        private static void UpdateDbWithException(Exception ex)//means only accessible within this Program class. 
        {
            string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=TwentyOneGame;
                                        Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                        TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //string that contains info about dB instance.
            string queryString = @"INSERT INTO Exceptions(ExceptionType,ExceptionMessage,TimeStamp) VALUES (@ExceptionType, @ExceptionMessage,
                                  @TimeStamp)"; //@ in values parenthesis means User session variable.

            //"using"opens up a connection to resource (dB) and automatically closes it when computer gets to end of curly brace.

            //creating new instance of Sql connection. 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);//initializing new command object.

                //Adding data type to the parameters to protect against sql injection(where user instead of giving their name
                //they give you a sql statement which will run on your dB). 

                command.Parameters.Add("@ExceptionType", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@ExceptionMessage", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@TimeStamp", System.Data.SqlDbType.DateTime);

                //Adding values to the dB. Parameters is a list. Square braces pick out what item in list to add 
                //value to. 

                command.Parameters["@ExceptionType"].Value = ex.GetType().ToString();
                command.Parameters["@ExceptionMessage"].Value = ex.Message;
                command.Parameters["@TimeStamp"].Value = DateTime.Now;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();


            }



        }


        //Reading from a database. Mapping class (exception entity) to dB

        private static List<ExceptionEntity> ReadExceptions()
        {

            string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=TwentyOneGame;
                                        Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                        TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            string queryString = @"Select Id, ExceptionType,ExceptionMessage, TimeStamp From Exceptions";

            List<ExceptionEntity> Exceptions = new List<ExceptionEntity>();//initializing list

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ExceptionEntity exception = new ExceptionEntity();
                    exception.Id = Convert.ToInt32(reader["Id"]);
                    exception.ExceptionType = Convert.ToString(reader["ExceptionType"]);
                    exception.ExceptionMessage = Convert.ToString(reader["ExceptionMessage"]);
                    exception.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);

                    Exceptions.Add(exception);
                }
                connection.Close();
            }
            return Exceptions;


        }



    }
}
    








