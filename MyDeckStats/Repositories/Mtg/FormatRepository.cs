using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class FormatRepository : RepositoryBase<Format, ApplicationDbContext>, IFromatRepository
    {
        public FormatRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
