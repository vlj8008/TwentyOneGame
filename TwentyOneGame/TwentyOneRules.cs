using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOneGame
{
    public class  TwentyOneRules
    {

        //naming convention for private class is to use underscore. 
        private static Dictionary<Face, int> _cardValues = new Dictionary<Face, int>()
        {
            //Instantiating the dictionary.
            [Face.Two] = 2, //private object as only going to be accessed within this class.
            [Face.Three] = 3,
            [Face.Four] = 4,
            [Face.Five] = 5,
            [Face.Six] = 6,
            [Face.Seven] = 7,
            [Face.Eight] = 8,
            [Face.Nine] = 9,
            [Face.Ten] = 10,
            [Face.Jack] = 10,
            [Face.Queen] = 10,
            [Face.King] = 10,
            [Face.Ace] = 1
        }; 

        private static int[] GetAllPossibleHandValues(List<Card> Hand)//returning integer array.
        {
            int aceCount = Hand.Count(x => x.Face==Face.Ace);//counting how many aces in players hand.
            //This determines how many values there could be. 

            int[] result = new int[aceCount + 1];//result is integer array. To create new array must state how many in array (aceCount +1)
            int value = Hand.Sum(x => _cardValues[x.Face]);//lowest count if ace has value of 1 (default value). Lambda expression looks up value in Dict
            result[0] = value;//assign "value" to first entry in integer array. 

            if (result.Length == 1)//if there is only one value (means no aces.)
            {
                return result;
            }

            for(int i = 1; i < result.Length; i++)//iterating through to give different values to ace. 
            {
                value = value + (i * 10);//value is lowest possible value with aces equalling one(the default value). 
                result[i] = value;
            }
            return result;
        }



        public static bool CheckForBlackJack(List<Card> Hand)
        {
            int[] possibleValues = GetAllPossibleHandValues(Hand);//create integer array of possible values
            int value = possibleValues.Max(); //get largest value.
            if (value == 21) return true;
            else return false;
        }

        public static bool IsBusted(List<Card> Hand)
        {
            int value = GetAllPossibleHandValues(Hand).Min();
            if (value > 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ShouldDealerStay(List<Card> Hand)
        {
            int[] possibleHandValues = GetAllPossibleHandValues(Hand);
            foreach (int value in possibleHandValues)
            {
                if (value >16 && value < 22)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool? CompareHands(List<Card> PlayerHand, List<Card> DealerHand)
        {
            int[] playerResults = GetAllPossibleHandValues(PlayerHand);
            int[] dealerResults = GetAllPossibleHandValues(DealerHand);

            int playerScore = playerResults.Where(x => x < 22).Max();//highest value that is also less than 22.
            int dealerScore = dealerResults.Where(x => x< 22).Max();

            if(playerScore > dealerScore)
            {
                return true;
            }

            else if (playerScore < dealerScore) return false;
            else return null;
        }
    }
}
