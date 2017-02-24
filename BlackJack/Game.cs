using System;

//Future plans:
//Add split command for duplicate initial cards
//Perhaps add chips for betting?

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
            Console.Write("Would you like to play again? (Enter 'Y' or 'N'): ");

            char input = Game.CollectValidInput("Yes or No");

            if(input == 'Y')
            {
                Console.Clear();   
                             
                deck.ShuffleDeck();
                player.EmptyHand();
                dealer.EmptyHand();

                Game.GameRound(player, dealer, deck);

                PlayAgain(player, dealer, deck);
            }
            else
            {
                double playerWinPercent = 100 * ((double)player.RoundScore / (player.RoundScore + dealer.RoundScore));
                double dealerWinPercent = 100 * ((double)dealer.RoundScore / (player.RoundScore + dealer.RoundScore));

                Console.WriteLine($"\n\nPlayer wins: {player.RoundScore}             Dealer wins: {dealer.RoundScore}");
                Console.WriteLine($"Win percent: {Math.Round(playerWinPercent, 2)}%         Win percent: {Math.Round(dealerWinPercent, 2)}%");
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

            foreach (Cards card in player.PlayerHand)
            {
                Console.Write($"[{card.Name}] ");
            }

            Console.WriteLine($"Total: {player.SumCardValues()}");


            if(player.SumCardValues() == 21)
            {
                Console.WriteLine("BLACKJACK! You won!!");
                player.RoundScore += 1;
                return 0;                
            }                        

            if (player.SumCardValues() > 21)
            {                                
                Console.WriteLine($"BUST! You lost with a total of {player.SumCardValues()}..");
                dealer.RoundScore += 1;
                return 0;
            }

            Console.Write("Stay or Hit? (Enter 'S' or 'H'): ");
            

            char input = Game.CollectValidInput("Stay or Hit");

            if (input == 'S')
            {
                if (dealer.didBustToSeventeen(dealer, deck) == true)
                {
                    player.RoundScore += 1;
                    dealer.WonOrLossOutput("dealerBust", dealer);
                    return 0;
                }
                else
                {
                    string outcome = Game.CompareHands(player, dealer);
                    dealer.WonOrLossOutput(outcome, dealer);                    
                }
                    
            }

            if (input == 'H')
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
            int dealerSum = dealer.SumCardValues();
            int playerSum = player.SumCardValues();

            if (dealerSum > playerSum && dealerSum < 22)
            {
                dealer.RoundScore += 1;
                return "dealerWon";
            }
            if(dealerSum == playerSum)
            {
                player.RoundScore += 1;
                dealer.RoundScore += 1;
                return "tie";
            }
            else
            {
                player.RoundScore += 1;
                return "playerWon";
            }
        }

        public static char CollectValidInput(string acceptedInput)
        {
            char char1;
            char char2;

            if (acceptedInput == "Stay or Hit")
            {
                char1 = 'S';
                char2 = 'H';
            }
            else
            {
                char1 = 'Y';
                char2 = 'N';
                acceptedInput = "Would you like to play again";
            }

            char input = Console.ReadKey().KeyChar;
            input = char.ToUpper(input);

            while (input != char1 && input != char2)
            {
                Console.Write($"\n\nSorry, that was an invalid option! Try again using '{char1}' or '{char2}' .\n");
                Console.Write($"{acceptedInput}? (Enter '{char1}' or '{char2}'): ");
                input = Console.ReadKey().KeyChar;
                input = char.ToUpper(input);
            }

            return input;
        }
    }
}
