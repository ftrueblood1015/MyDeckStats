using System.ComponentModel.DataAnnotations;

namespace MyDeckStats.Domain.Entities
{
    public class EntityBase
    {
        [Required]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
