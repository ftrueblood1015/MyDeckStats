namespace MyDeckStats.Domain.Entities.Mtg.Cards
{
    public class MtgSet : ScryFallImportBase
    {
        public string? Code { get; set; }

        public string? Uri { get; set; }

        public string? ScryfallUri { get; set; }

        public string? SearchUri { get; set; }

        public DateTime ReleasedAt { get; set; }

        public string? SetType { get; set; }

        public int CardCount { get; set; }

        public string? ParentSetCode { get; set; }

        public bool Digital { get; set; }

        public bool NonfoilOnly { get; set; }

        public bool FoilOnly { get; set; }

        public string? IconSvgUri { get; set; }
    }
}
