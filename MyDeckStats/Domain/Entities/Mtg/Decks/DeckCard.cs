using MyDeckStats.Domain.Entities.Mtg.Cards;

namespace MyDeckStats.Domain.Entities.Mtg.Decks
{
    public class DeckCard : TrackableEntityBase
    {
        public Guid MtgCardId { get; set; }
        public Guid DeckId { get; set; }
        public int Amount { get; set; }
        public MtgCard? MtgCard { get; set; }
        public Deck? Deck { get; set; }
    }
}
