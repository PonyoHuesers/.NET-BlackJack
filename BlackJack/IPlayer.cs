using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    interface IPlayer
    {
        int SumCardValues();
        List<Cards> PlayerHand { get; set; }
        void EmptyHand();
    }
}
