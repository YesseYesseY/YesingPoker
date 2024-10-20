using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesingPokerCommon.Cards
{
    public class Card
    {
        CardSuit Suit;
        CardValue Value;

        public Card(CardSuit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString()
        {
            if (Suit == CardSuit.Joker)
                return "Joker";
            return $"{Enum.GetName(typeof(CardValue), Value)} of {Enum.GetName(typeof(CardSuit), Suit)}";
        }
    }
}
