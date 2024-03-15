using System.ComponentModel.DataAnnotations;

namespace MyDeckStats.Domain.Entities.Mtg.Cards
{
    public class ColorIdentity : EntityBase
    {
        [Required]
        public Guid MtgCardId { get; set; }

        public MtgCard? MtgCard { get; set; }
    }
}
