using System.ComponentModel.DataAnnotations;

namespace MyDeckStats.Domain.Entities.Mtg.Cards
{
    public class SetCard : EntityBase
    {
        [Required]
        public Guid MtgCardId { get; set; }

        [Required]
        public Guid MtgSetId { get; set; }

        public MtgCard? MtgCard { get; set; }

        public MtgSet? MtgSet { get; set; }
    }
}
