using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Player
    {
        public static List<Cards> PlayerHand { get; set; }
        public static int HandCount { get; set; } = 0;

        public Player()
        {
            PlayerHand = new List<Cards>();
        }

        public static int Count()
        {
            HandCount = 0;
            foreach (var card in PlayerHand)
            {
                HandCount += card.Value;
            }

            return HandCount;
        }
    }

    class Dealer
    {
        public static List<Cards> DealerHand { get; set; }
        public static int HandCount { get; set; }

        public Dealer()
        {
            DealerHand = new List<Cards>();
        }

        public static int Count()
        {
            HandCount = 0;
            foreach (var card in DealerHand)
            {
                HandCount += card.Value;
            }

            return HandCount;
        }

        public static bool bustedToSeventeen()
        {
            while(Dealer.Count() <= 16)
            {
                Deck.DealerDraw();
            }

            if (Dealer.Count() < 22)
            {
                return false;
            }                
            else
            {
                if (Ace.IsAceInHand("Dealer"))
                {
                    Ace.ReplaceAceValue("Dealer");
                    bustedToSeventeen();
                }               

                return true;
            }            
        }

        public static void WonOrLossOutput(string outcome)
        {
            if(outcome == "didBust")
            {
                Console.WriteLine();
                Console.WriteLine("Dealer busted! You win!!");
                Console.Write("Dealer's Cards: ");
                foreach (var card in Dealer.DealerHand)
                {
                    Console.Write($"[{card.Name}] ");
                }
                Console.WriteLine("Total: " + Dealer.Count());
            }
            if(outcome == "lowerScoreThanPlayer")
            {
                Console.WriteLine();
                Console.WriteLine("Your score beat out the Dealer! You win!!");
                Console.Write("Dealer's Cards: ");
                foreach (var card in Dealer.DealerHand)
                {
                    Console.Write($"[{card.Name}] ");
                }
                Console.WriteLine("Total: " + Dealer.Count());
            }
            if(outcome == "tie")
            {
                Console.WriteLine();
                Console.WriteLine("Oh dear... Split pot.");
                Console.Write("Dealer's Cards: ");
                foreach (var card in Dealer.DealerHand)
                {
                    Console.Write($"[{card.Name}] ");
                }
                Console.WriteLine("Total: " + Dealer.Count());
            }
            if(outcome == "won")
            {
                Console.WriteLine();
                Console.WriteLine("The Dealer won...");
                Console.Write("Dealer's Cards: ");
                foreach (var card in Dealer.DealerHand)
                {
                    Console.Write($"[{card.Name}] ");
                }
                Console.WriteLine("Total: " + Dealer.Count());
            }
        }
    }
}
