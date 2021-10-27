using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Deck
    {
        public Deck() //Constructors always same name as class, and always at top above properties. 
        {

            Cards = new List<Card>(); //"Card" refers to the property of the Deck class (below). 

            for (int i = 0; i < 13; i++) //access integer data type for underlying enum. 
            {
                for (int j = 0; j<4; j++)
                {
                    Card card = new Card();
                    card.Face = (Face)i; //cast integer to string. Put datatype casting to inside parentheses. 
                    // ie casting the integer value "i" to datatype "Face" .

                    card.Suit = (Suit)j;
                    Cards.Add(card);
                }
            }

            
        } 
        //*Creating properties:

        //One property of deck is a list of cards. 
        public List<Card> Cards { get; set; }

        
        public void Shuffle(int times = 1)

        {
            

            for (int i = 0; i < times; i++)
            {
                
                List<Card> TempList = new List<Card>();
                Random random = new Random();

                while (Cards.Count > 0)
                {

                    //method to get a random number from the deck. Parameters are
                    // min index = 0 and max = deck count (52)
                    int randomIndex = random.Next(0, Cards.Count);
                    //add to TempList
                    TempList.Add(Cards[randomIndex]);
                    //Remove card from deck list
                    Cards.RemoveAt(randomIndex);
                }

                Cards = TempList;

            }
            

        }

    }
}



