using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyDeckStats.Domain.Entities
{
    [Index(nameof(ScryfallId), IsUnique = true)]
    public class ScryFallImportBase : EntityBase
    {
        [Required]
        public string? ScryfallId { get; set; }
    }
}
