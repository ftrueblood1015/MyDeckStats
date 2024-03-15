using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class MtgKeywordRepository : RepositoryBase<MtgKeyword, ApplicationDbContext>, IKeywordRepository
    {
        public MtgKeywordRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
