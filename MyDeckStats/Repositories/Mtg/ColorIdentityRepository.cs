using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class ColorIdentityRepository : RepositoryBase<ColorIdentity, ApplicationDbContext>, IColorIdentityRepository
    {
        public ColorIdentityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
