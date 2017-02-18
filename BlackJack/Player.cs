using System;
using System.Collections.Generic;

namespace BlackJack
{
    class Player : IPlayer
    {
        public List<Cards> PlayerHand { get; set; }
        public int HandCount { get; private set; } = 0;

        public Player()
        {
            PlayerHand = new List<Cards>();
        }

        public int SumCardValues()
        {
            HandCount = 0;
            foreach (Cards card in PlayerHand)
            {
                HandCount += card.Value;
            }

            return HandCount;
        }

        public void EmptyHand()
        {
            PlayerHand.Clear();
        }
    }

    class Dealer : IPlayer
    {
        public List<Cards> PlayerHand { get; set; }
        public int HandCount { get; private set; }

        public Dealer()
        {
            PlayerHand = new List<Cards>();
        }

        public int SumCardValues()
        {
            HandCount = 0;
            foreach (Cards card in PlayerHand)
            {
                HandCount += card.Value;
            }

            return HandCount;
        }
        
        public bool didBustToSeventeen(Dealer dealer, Deck deck)
        {

            while(dealer.SumCardValues() <= 16)
            {
                deck.PlayerDraw(dealer);
            }

            if (Ace.IsAceInHand(dealer) && dealer.SumCardValues() > 21)
            {
                Ace.ReplaceAceValue(dealer);
                didBustToSeventeen(dealer, deck);
            }

            if (dealer.SumCardValues() > 21)
            {
                return true;
            }

            return false;            
        }

        public void EmptyHand()
        {
            PlayerHand.Clear();
        }

        public void WonOrLossOutput(string outcome, Dealer dealer)
        {
            Console.WriteLine();

            if(outcome == "dealerBust")
            {
                Console.WriteLine();
                Console.WriteLine("Dealer busted! You win!!");
                Console.Write("Dealer's Cards: ");
                foreach (Cards card in dealer.PlayerHand)
                {
                    Console.Write($"[{card.Name}] ");
                }
                Console.WriteLine("Total: " + dealer.SumCardValues());
            }
            if(outcome == "playerWon")
            {
                Console.WriteLine();
                Console.WriteLine("Your score beat out the Dealer! You win!!");
                Console.Write("Dealer's Cards: ");
                foreach (Cards card in dealer.PlayerHand)
                {
                    Console.Write($"[{card.Name}] ");
                }
                Console.WriteLine("Total: " + dealer.SumCardValues());
            }
            if(outcome == "tie")
            {
                Console.WriteLine();
                Console.WriteLine("Oh dear... Split pot.");
                Console.Write("Dealer's Cards: ");
                foreach (Cards card in dealer.PlayerHand)
                {
                    Console.Write($"[{card.Name}] ");
                }
                Console.WriteLine("Total: " + dealer.SumCardValues());
            }
            if(outcome == "dealerWon")
            {
                Console.WriteLine();
                Console.WriteLine("The Dealer won...");
                Console.Write("Dealer's Cards: ");
                foreach (Cards card in dealer.PlayerHand)
                {
                    Console.Write($"[{card.Name}] ");
                }
                Console.WriteLine("Total: " + dealer.SumCardValues());
            }
        }
    }
}
