using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Repositories.Mtg;
using MyDeckStats.Services.Users;

namespace MyDeckStats.Services.Mtg
{
    public class MtgKeywordService : ServiceBase<MtgKeyword, IKeywordRepository>, IMtgKeywordService
    {
        public MtgKeywordService(IKeywordRepository repo) : base(repo)
        {
        }
    }
}
