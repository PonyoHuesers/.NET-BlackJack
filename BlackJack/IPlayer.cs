using System.Collections.Generic;

namespace BlackJack
{
    interface IPlayer
    {
        int SumCardValues();
        List<Cards> PlayerHand { get; set; }
        void EmptyHand();
    }
}
