using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwentyOneGame
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

            Console.WriteLine("Welcome to the Royal Casino. Let's start by getting your name?");
            
            string playerName = Console.ReadLine();
            Console.WriteLine($"Hello {playerName}, and how much money did you bring today?");

            int bank = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Would you like to play a game of Twenty One right now?");
            string answer = Console.ReadLine().ToLower();

            if (answer == "yes" || answer == "y" || answer == "ya" || answer == "yeah")
            {
                Console.WriteLine("Great");

                Player player = new Player(playerName, bank);

                Game game = new TwentyOneGame();//polymorphism so can access overloaded operator

                game = game + player;

                player.isActivelyPlaying = true;

                while (player.isActivelyPlaying && bank > 0)
                {
                    game.Play();
                }

                game = game - player;
                Console.WriteLine("Thanks for playing.");
                
            }

            Console.WriteLine("Feel free to look around the casino. Bye for now");
            Console.ReadLine();

        }

        

    }
    
}






