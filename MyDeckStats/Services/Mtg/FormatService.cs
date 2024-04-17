using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Users;

namespace MyDeckStats.Services.Mtg
{
    public class FormatService : ServiceBase<Format, IFromatRepository>, IFromatService
    {
        public FormatService(IFromatRepository repo) : base(repo)
        {
        }
    }
}
