using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class GuildRepository : RepositoryBase<Guild, ApplicationDbContext>, IGuildRepository
    {
        public GuildRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
