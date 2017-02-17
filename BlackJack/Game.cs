using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program 
    {        
        static void Main()
        {
            Player player = new Player();
            Dealer dealer = new Dealer();                        
            Deck deck = new Deck();

            Game.GameRound(player, dealer, deck);

            PlayAgain(player, dealer, deck);
        }

        private static void PlayAgain(Player player, Dealer dealer, Deck deck)
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to play again? (Enter 'Y' or 'N'):");

            var input = Console.ReadLine().ToUpper();

            if(input == "Y")
            {
                Console.Clear();   
                             
                deck.ShuffleDeck(deck);
                player.EmptyHand();
                dealer.EmptyHand();

                Game.GameRound(player, dealer, deck);

                PlayAgain(player, dealer, deck);
            }
        }
    }

    class Game
    {
        public static int GameRound(Player player, Dealer dealer, Deck deck)
        {
            if (player.PlayerHand.Count == 0 || dealer.PlayerHand.Count == 0)
            {
                deck.FreshDeal(player, dealer);

                //If the first two draws are Aces, it prevents busting with a 22.
                deck.AcesFirstTwoCards(player, dealer);
            }            

            Console.WriteLine($"Dealer's Cards: [{dealer.PlayerHand[0].Name}] [Hidden]");
            Console.Write("Player's Cards: ");

            foreach (var card in player.PlayerHand)
            {
                Console.Write($"[{card.Name}] ");
            }

            Console.WriteLine($"Total: {player.SumCardValues()}");


            if(player.SumCardValues() == 21)
            {
                Console.WriteLine("BLACKJACK! You won!!");
                return 0;                
            }                        

            if (player.SumCardValues() > 21)
            {                
                Console.WriteLine($"BUST! You lost with a total of {player.SumCardValues()}..");
                return 0;
            }

            Console.Write("Stay or Hit? (Enter 'S' or 'H'): ");

            var input = Console.ReadLine().ToUpper();

            if (input == "S")
            {
                if (dealer.didBustToSeventeen(dealer, deck) == true)
                {
                    dealer.WonOrLossOutput("dealerBust", dealer);
                    return 0;
                }
                else
                {
                    var outcome = Game.CompareHands(player, dealer);
                    dealer.WonOrLossOutput(outcome, dealer);                    
                }
                    
            }

            if (input == "H")
            {
                Console.Clear();
                deck.PlayerDraw(player);

                if(player.SumCardValues() > 21 && Ace.IsAceInHand(player))
                {
                    Ace.ReplaceAceValue(player);
                    GameRound(player, dealer, deck);
                }
                else
                {
                    GameRound(player, dealer, deck);
                }
            }

            return 0;
            
        }

        public static string CompareHands(Player player, Dealer dealer)
        {
            var dealerSum = dealer.SumCardValues();
            var playerSum = player.SumCardValues();

            if (dealerSum > playerSum && dealerSum < 22)
            {
                return "dealerWon";
            }
            if(dealerSum == playerSum)
            {
                return "tie";
            }
            else
            {
                return "playerWon";
            }
        }
    }
}
