using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOneGame
{
    public class TwentyOneGame : Game //":Game" means we inherit from the Game class
    {

        public TwentyOneDealer Dealer { get; set; }

        public override void Play()//abstract method from Game class means we MUST implement it. 
        {
            Dealer = new TwentyOneDealer();//instantiate a new 21 dealer.

            foreach (Player player in Players)//reset the players at the game.
            {
                player.Hand = new List<Card>();
                player.Stay = false;
            }
            Dealer.Hand = new List<Card>(); //new hand for dealer
            Dealer.Stay = false;
            Dealer.Deck = new Deck();
            Dealer.Deck.Shuffle();

            Console.WriteLine("Place your bet!");

            foreach(Player player in Players)
            {
                int bet = Convert.ToInt32(Console.ReadLine());
                bool successfullyBet = player.Bet(bet);

                if (!successfullyBet)
                {
                    return; //we are in a void method, so this return means end this method (rather than return
                    //a value which it can't because it is a void method. Program says "You don't have enough, place your
                    //bet"
                }
                Bets[player] = bet;//enter bet amount into dictionary. Dictionary is key, value pair. Key=Player, Value= amount.
                //Dictionary is part of Game class, so was inherited. 
            }

            for(int i = 0; i<2; i++)//i=0 is starting point, and do this twice (while i<2).
            {
                Console.WriteLine("dealing....");
                foreach (Player player in Players)
                {
                    Console.WriteLine("{0}:", player.Name);
                    Dealer.Deal(player.Hand);

                    if (i == 1) //checking to see if blackjack has happened after second card dealt. 
                    {
                        bool blackJack = TwentyOneRules.CheckForBlackJack(player.Hand);
                        if (blackJack)
                        {
                            Console.WriteLine("Blackjack ! {0} wins {1}", player.Name, Bets[player]);
                            player.Balance = Convert.ToInt32((Bets[player] * 1.5) + Bets[player]);
                            return;
                        }
                    }

                }
                Console.Write("Dealer: ");
                Dealer.Deal(Dealer.Hand);
                if(i == 1)
                {
                    bool blackJack = TwentyOneRules.CheckForBlackJack(Dealer.Hand);
                    if (blackJack)
                    {
                        Console.WriteLine("Dealer has BlackJack ! Everyone loses !");

                        foreach (KeyValuePair<Player,int> entry in Bets)
                        {
                            Dealer.Balance += entry.Value;
                        }
                        return;
                    }

                }
            }
            foreach(Player player in Players)
            {
                while (!player.Stay)
                {
                    Console.WriteLine("Your cards are: ");
                    foreach(Card card in player.Hand)
                    {
                        Console.Write("{0} ", card.ToString());//the ToString method was customized by us. It was overidden. 
                    }
                    Console.WriteLine("\n\nHit or stay?");
                    string answer = Console.ReadLine().ToLower();

                    if (answer == "stay")
                    {
                        player.Stay = true;
                        break;//break the loop. 
                    }

                    else if (answer == "hit")
                    {
                        Dealer.Deal(player.Hand);
                    }
                    bool busted = TwentyOneRules.IsBusted(player.Hand);

                    if (busted)
                    {

                        Dealer.Balance += Bets[player];
                        Console.WriteLine("{0} Busted ! You lose your bet of {1}. Your balance is now {2}.", player.Name,Bets[player],player.Balance);
                        Console.WriteLine("Would you like to play again");
                        string answer1 = Console.ReadLine().ToLower();

                        if (answer1 == "yes"|| answer1 == "yeah"||answer1=="y")
                        {
                            player.isActivelyPlaying = true;
                            return;
                        }

                        else
                        {
                            player.isActivelyPlaying = false;
                            return;
                        }


                    }
                }
            }
            Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand);
            Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);

            while (!Dealer.Stay && !Dealer.isBusted)
            {
                Console.WriteLine("Dealer is hitting....");
                Dealer.Deal(Dealer.Hand);
                Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand);
                Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);
            }

            if (Dealer.Stay)
            {
                Console.WriteLine("Dealer is staying.");
            }

            if (Dealer.isBusted)
            {
                Console.WriteLine("Dealer busted !");
                foreach (KeyValuePair<Player, int> entry in Bets)
                {
                    Console.WriteLine("{0} won {1}!", entry.Key.Name, entry.Value);
                    Players.Where(x => x.Name == entry.Key.Name).First().Balance += (entry.Value * 2);//"Where" produces a list. First() grabs first name on list 
                    Dealer.Balance -= entry.Value;                                                                                  
                }
                return;
            }
            foreach(Player player in Players)
            {
                //nullable boolean.

                bool? playerWon = TwentyOneRules.CompareHands(player.Hand,Dealer.Hand);

                if(playerWon == null)
                {
                    Console.WriteLine("Push! No one wins ");
                    player.Balance += Bets[player];
                    
                }

                else if (playerWon == true)
                {
                    Console.WriteLine("{0} won {1} ",player.Name,Bets[player]);
                    player.Balance += (Bets[player] * 2);
                    Dealer.Balance -= Bets[player];
                }

                else
                {
                    Console.WriteLine("Dealer wins{0}!", Bets[player]);
                    Dealer.Balance += Bets[player];

                }

                Console.WriteLine("Play again ?");
                string answer = Console.ReadLine().ToLower();

                if (answer == "yes" || answer == "y")
                {
                    player.isActivelyPlaying = true;
                    return;
                }
                else
                {
                    player.isActivelyPlaying = false;
                    return;
                }
            }
            

               
        }

        public override void ListPlayers()
        {

            Console.WriteLine("21 Players: ");
            base.ListPlayers();
        }

        public void WalkAway(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
