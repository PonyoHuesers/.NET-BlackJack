using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Cards
    {
        public string Name { get; set; }
        public virtual int Value { get; set; }
        public bool IsDrawn { get; set; } = false;
        public virtual bool IsMaxValue { get; set; } = true;
    }

    class Ace : Cards
    {
        public override int Value { get; set; } = 11;
        public override bool IsMaxValue { get; set; } = true;

        public static bool IsAceInHand(string player)
        {
            if(player == "Player")
            {
                foreach (Cards card in Player.PlayerHand)
                {
                    if (card.Name == "A" && card.IsMaxValue == true)
                        return true;
                }

                return false;
            }
            else
            {
                foreach (Cards card in Dealer.DealerHand)
                {
                    if (card.Name == "A" && card.IsMaxValue == true)
                        return true;
                }

                return false;
            }
            
        }

        public static void ReplaceAceValue(string player)
        {
            if(player == "Player")
            {
                Ace firstAce = (Ace)Player.PlayerHand.First(c => c.Name == "A" && c.IsMaxValue == true);
                firstAce.Value = 1;
                firstAce.IsMaxValue = false;
            }
            else
            {
                Ace firstAce = (Ace)Dealer.DealerHand.First(c => c.Name == "A" && c.IsMaxValue == true);
                firstAce.Value = 1;
                firstAce.IsMaxValue = false;
            }
            
        }
    }
}
