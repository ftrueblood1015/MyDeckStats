using Microsoft.EntityFrameworkCore;
using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class MtgCardRepository : RepositoryBase<MtgCard, ApplicationDbContext>, IMtgCardRepository
    {
        public MtgCardRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
