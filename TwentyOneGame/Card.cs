using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOneGame
{
    public struct Card //changed from class to struct to be a value type instead of reference type. 
        //don't want card changing like reference types change and card will not be inherited. 

    {   //Constructor method assigns values to object upon creation.
        //MUST be same name as class (Card). That is how C # knows it is Constructor.
        //Syntax: Access specifier, className, (parameters)

        //* Creating Properties (or state) (short hand way):

        public Suit Suit { get; set; } // "public" is access modifier, then data type (Suit), then name of property,
        // you can "get" or "set" this property.
        public Face Face { get; set; }

        public override string ToString() //custom "to string method"
        {
            return string.Format("{0} of {1}", Face, Suit);
        }
    }

    public enum Suit //Defining enum. "Suit" is name, and datatype name. 
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades,
    }

    public enum Face
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine, 
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
}
