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
            PlayAgain();
        }

        private static void PlayAgain()
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to play again? (Enter 'Y' or 'N'):");
            var input = Console.ReadLine();

            if(input == "Y")
            {
                Console.Clear();
                Game.GameRound(new Player(), new Dealer(), new Deck());
                PlayAgain();
            }
        }
    }

    class Game
    {
        public static bool GameRound(Player player, Dealer dealer, Deck deck)
        {
            if (Player.PlayerHand.Count == 0)
                deck.FreshDeal();

            //If first two draws are Aces, it prevents getting busted with a 22.
            if (Player.PlayerHand[0].Name == "A" && Player.PlayerHand[1].Name == "A" && Player.PlayerHand.Count == 2) { Ace.ReplaceAceValue("Player"); }
            if (Dealer.DealerHand[0].Name == "A" && Dealer.DealerHand[1].Name == "A" && Dealer.DealerHand.Count == 2) { Ace.ReplaceAceValue("Dealer"); }

            Console.WriteLine($"Dealer's Cards: [{Dealer.DealerHand[0].Name}] [Hidden]");
            Console.Write("Player's Cards: ");
            foreach (var card in Player.PlayerHand)
            {
                Console.Write($"[{card.Name}] ");
            }
            Console.WriteLine($"Total: {Player.Count()}");


            if(Player.Count() == 21)
            {
                Console.WriteLine("BLACKJACK! You won!!");
                return true;                
            }                        

            if (Player.Count() > 21)
            {                
                Console.WriteLine($"BUST! You lost with a total of {Player.Count()}..");
                return false;
            }

            Console.Write("Stay or Hit? (Enter 'S' or 'H'): ");

            var input = Console.ReadLine();

            if (input == "S")
            {
                var isBusted = Dealer.bustedToSeventeen();
                if (isBusted == true)
                {
                    Dealer.WonOrLossOutput("didBust");
                    return true;
                }
                else
                {
                    var outcome = Game.DealerWon();
                    Dealer.WonOrLossOutput(outcome);                    
                }
                    
            }

            if (input == "H")
            {
                Console.Clear();
                deck.PlayerDraw();

                if(Player.Count() > 21 && Ace.IsAceInHand("Player"))
                {
                    Ace.ReplaceAceValue("Player");
                    GameRound(player, dealer, deck);
                }
                else
                {
                    GameRound(player, dealer, deck);
                }
            }

            return false;
            
        }

        public static string DealerWon()
        {
            var dealerSum = Dealer.Count();
            var playerSum = Player.Count();

            if (dealerSum > playerSum && dealerSum < 22)
            {
                return "won";
            }
            if(dealerSum == playerSum)
            {
                return "tie";
            }
            else
            {
                return "lowerScoreThanPlayer";
            }
        }
    }
}
