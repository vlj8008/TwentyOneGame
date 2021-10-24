using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwentyOneGame
{
    public class Dealer
    {

        //Properties:

        public string Name { get; set; }
        public Deck Deck { get; set; }
        public int Balance { get; set; }//number of chips dealer has. 

        //Methods:

        public void Deal(List<Card> Hand)
        {
            Hand.Add(Deck.Cards.First());//add first card from deck to dealers hand. 

            string card = string.Format(Deck.Cards.First().ToString() + "\n");
            Console.WriteLine(card);

            //memory manager handles so don't lose memory
            using (StreamWriter file = new StreamWriter(@"C:\Users\vicky\OneDrive\Documents\GitHub\Basic_C#_Programs\log.txt", true))
            {
                file.WriteLine(DateTime.Now); //using DateTime to write exact time. 
                file.WriteLine(card);
            }
                Deck.Cards.RemoveAt(0);//remove that card from particular index (0).
        }
    }
}
