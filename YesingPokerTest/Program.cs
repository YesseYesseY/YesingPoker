using YesingPokerCommon.Cards;

var deck = new CardDeck(2);

Card? card = null;
do
{
    card = deck.DrawCard();
    Console.WriteLine(card);
} while (card != null);