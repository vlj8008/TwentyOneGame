using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Player
    {
        //Constructor

        //Constructor Call Chain
        //Helps re-use Constructors. 

        public Player(string name) : this(name, 100) //: means we inherit from original constructor. 
        {
        } 

        //original Constructor
        public Player(string name, int beginningBalance)
        {
            Name = name;//Properties
            Balance = beginningBalance;
            Hand = new List<Card>();//initializing the list of data type Card to the variable Hand so we can use it.
            
        }

        //Properties:
        private List<Card> _hand = new List<Card>();
        public List<Card> Hand { get { return _hand; } set { _hand = value; } }
        public int Balance { get; set; }
        public string Name { get; set; }
        public bool isActivelyPlaying { get; set; }
        public bool Stay { get; set; }
        public Guid Id { get; set; }

        //Method to check to see if player has enough money to bet. 
        public bool Bet(int amount)
        {
            if (Balance- amount <0)
            {
                Console.WriteLine("You don't have enough to place bet that size");
                return false;
            }

            else
            {
                Balance = Balance - amount;
                return true;
            }

        

        }


        //Overloading an operator method (plus operator)

        public static Game operator+(Game game, Player player) //"operator" is C sharp reserved word. 
        {
            game.Players.Add(player); //takes "game" and adds player to it
            return game; //returns "game"
            
        }

        //Overloading the minus operator.

        public static Game operator-(Game game, Player player)
        {
            game.Players.Remove(player);
            return game;
        }

        
    }
}
