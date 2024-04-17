using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Users;

namespace MyDeckStats.Services.Mtg
{
    public class GuildService : ServiceBase<Guild, IGuildRepository>, IGuildService
    {
        public GuildService(IGuildRepository repo) : base(repo)
        {
        }
    }
}
