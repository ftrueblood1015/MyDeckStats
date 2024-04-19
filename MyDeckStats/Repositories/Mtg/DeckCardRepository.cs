using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class DeckCardRepository : RepositoryBase<DeckCard, ApplicationDbContext>, IDeckCardRepository
    {
        public DeckCardRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
