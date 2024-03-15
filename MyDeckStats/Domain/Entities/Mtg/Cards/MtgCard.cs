using System.ComponentModel.DataAnnotations;

namespace MyDeckStats.Domain.Entities.Mtg.Cards
{
    public class MtgCard : EntityBase
    {
        public Guid OracleId { get; set; }

        public string? ScryfallUri { get; set; }

        public string? ColorIdentity { get; set; }

        public string? manaCost { get; set; }

        public int ConvertedManaCost { get; set; }

        public string? Type { get; set; }

        public string? OracleText { get; set; }

        public int? Power {  get; set; }

        public int Toughness { get; set; }

        public string? Rarity { get; set; }

        public int EdhrecRank { get; set; }

        public int PennyRank { get; set; }

        public bool ProducesMana { get; set; }

        [Required]
        public string? Slug { get; set; }
    }
}
