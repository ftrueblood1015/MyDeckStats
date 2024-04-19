using MyDeckStats.Domain.Entities.Mtg.Cards;

namespace MyDeckStats.Domain.Entities.Mtg.Decks
{
    public class Deck : TrackableEntityBase
    {
        public Guid FormatId { get; set; }

        public Guid GuildId { get; set; }

        public Guid MtgCardId { get; set; }

        public Format? Format { get; set; }

        public Guild? Guild { get; set; }

        public MtgCard? Card { get; set; }
    }
}
