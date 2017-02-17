using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Deck
    {
        public List<Cards> DeckList { get; set; }
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

        public void FreshDeal(Player player, Dealer dealer)
        {
            for (int i = 0; i < 2; i++)
            {
                PlayerDraw(player);
                PlayerDraw(dealer);
            }
        }

        public void PlayerDraw(IPlayer player)
        {
            var drawnCard = DeckList[_random.Next(52)];
            if (drawnCard.IsDrawn)
            {
                PlayerDraw(player);
            }
            else
            {
                player.PlayerHand.Add(drawnCard);
                drawnCard.IsDrawn = true;
            }
        }

        public void ShuffleDeck(Deck deck)
        {
            foreach (var card in DeckList)
            {
                if(card.IsDrawn == true)
                {
                    card.IsDrawn = false;
                }
                if(card is Ace && card.IsMaxValue == false)
                {
                    card.Value = 11;
                    card.IsMaxValue = true;
                }                
            }
        }

        public void AcesFirstTwoCards(Player player, Dealer dealer)
        {
            if (player.PlayerHand[0].Name == "A" && player.PlayerHand[1].Name == "A")
            {
                Ace.ReplaceAceValue(player);
            }

            if (dealer.PlayerHand[0].Name == "A" && dealer.PlayerHand[1].Name == "A")
            {
                Ace.ReplaceAceValue(dealer);
            }
        }
    }
}
