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

        public static bool IsAceInHand(IPlayer player)
        {
            foreach (Cards card in player.PlayerHand)
            {
                if (card.Name == "A" && card.IsMaxValue == true)
                    return true;
            }

            return false;
        }

        public static void ReplaceAceValue(IPlayer player)
        {
            Ace firstAce = (Ace)player.PlayerHand.First(c => c.Name == "A" && c.IsMaxValue == true);
            firstAce.Value = 1;
            firstAce.IsMaxValue = false;            
        }
    }
}
