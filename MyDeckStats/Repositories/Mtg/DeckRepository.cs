using Microsoft.EntityFrameworkCore;
using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class DeckRepository : RepositoryBase<Deck, ApplicationDbContext>, IDeckRepository
    {
        public DeckRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Deck> GetAll()
        {
            return Context.Set<Deck>().Include(g => g.Guild).Include(f => f.Format).Include(c => c.Card);
        }
    }
}
