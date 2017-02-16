using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Deck
    {
        public static List<Cards> DeckList { get; set; }
        private static readonly Random _random = new Random();
        private readonly Dictionary<int, string> _nameLookup = new Dictionary<int, string>
        {
            { 11, "J"},
            { 12, "Q"},
            { 13, "K"},
            { 14, "A"}
        };        
        
        public Deck()
        {
            DeckList = new List<Cards>(52);
            CreateDeck(DeckList);
        }
        
        private List<Cards> CreateDeck(List<Cards> deckList)
        {
            for(int numOfSuits = 0; numOfSuits < 4; numOfSuits++)
            {
                for(int cardValue = 2; cardValue < 15; cardValue++)
                {
                    var name = cardValue > 10 ? _nameLookup[cardValue] : cardValue.ToString();
                    var value = cardValue > 10 ? 10 : cardValue;
                    deckList.Add(new Cards { Name = name, Value = value });
                }
            }

            InsertAces(deckList);

            return deckList;
        }

        private void InsertAces(List<Cards> deckListAce)
        {
            deckListAce.RemoveAll(c => c.Name == "A");

            for (int i = 0; i < 4; i++)
            {
                deckListAce.Add(new Ace { Name = "A" });
            }
            
        }

        public void FreshDeal()
        {
            for (int i = 0; i < 2; i++)
            {
                PlayerDraw();
                DealerDraw();
            }

            //Code below used for testing with first two draws being Aces
            //
            //PlayerDraw();
            //Dealer.DealerHand.Add(DeckList[50]);
            //PlayerDraw();
            //Dealer.DealerHand.Add(DeckList[51]);
            //DeckList[50].IsDrawn = true;
            //DeckList[51].IsDrawn = true;
        }

        public void PlayerDraw()
        {
            var drawnCard = DeckList[_random.Next(52)];
            if (drawnCard.IsDrawn)
            {
                PlayerDraw();
            }
            else
            {
                Player.PlayerHand.Add(drawnCard);
                drawnCard.IsDrawn = true;
            }
        }

        public static void DealerDraw()
        {
            var drawnCard = DeckList[_random.Next(52)];
            if (drawnCard.IsDrawn)
            {
                DealerDraw();
            }
            else
            {
                Dealer.DealerHand.Add(drawnCard);
                drawnCard.IsDrawn = true;
            }
        }
    }
}
