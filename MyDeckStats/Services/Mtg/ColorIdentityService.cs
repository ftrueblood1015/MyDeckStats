using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Users;

namespace MyDeckStats.Services.Mtg
{
    public class ColorIdentityService : ServiceBase<ColorIdentity, IColorIdentityRepository>, IColorIdentityService
    {
        public ColorIdentityService(IColorIdentityRepository repo) : base(repo)
        {
        }
    }
}
