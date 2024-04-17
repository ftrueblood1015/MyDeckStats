namespace MyDeckStats.Domain.Entities
{
    public class TrackableEntityBase : EntityBase
    {
        public DateTime? Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? CreatedBy {  get; set; }
        public string? UpdatedBy { get; set; }
    }
}
