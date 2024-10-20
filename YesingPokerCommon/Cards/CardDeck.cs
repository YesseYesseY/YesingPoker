namespace YesingPokerCommon.Cards
{
    public class CardDeck
    {
        private List<Card> _cards = new List<Card>(52);

        public CardDeck(int jokers = 0)
        {
            var suits = Enum.GetValues(typeof(CardSuit));
            var values = Enum.GetValues(typeof(CardValue));

            foreach (CardSuit suit in suits)
            {
                if (suit == CardSuit.Joker)
                    continue;

                foreach (CardValue value in values)
                {
                    if (value == CardValue.Joker)
                        continue;

                    _cards.Add(new Card(suit, value));
                }
            }

            for (int i = 0; i < jokers; i++)
            {
                _cards.Add(new Card(CardSuit.Joker, CardValue.Joker));
            }
        }

        public Card? DrawCard(bool topCard = true)
        {
            if (_cards.Count == 0)
                return null;

            Card card;
            if (topCard)
            {
                card = _cards.First();
                _cards.RemoveAt(0);
            }
            else
            {
                card = _cards.Last();
                _cards.RemoveAt(_cards.Count - 1);
            }
            return card;
        }
    }
}
