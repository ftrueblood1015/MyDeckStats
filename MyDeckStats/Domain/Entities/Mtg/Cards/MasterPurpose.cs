namespace MyDeckStats.Domain.Entities.Mtg.Cards
{
    public class MasterPurpose : EntityBase
    {
        public bool IsActive { get; set; }

        public string? IncludeTerms { get; set; }

        public string? ExcludeTerms { get; set; }
    }
}
