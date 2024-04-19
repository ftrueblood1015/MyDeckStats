using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;

namespace MyDeckStats.Services.Mtg
{
    public class DeckCardService : TrackableServiceBase<DeckCard, IDeckCardRepository>, IDeckCardService
    {
        public DeckCardService(IDeckCardRepository repo) : base(repo)
        {
        }
    }
}
