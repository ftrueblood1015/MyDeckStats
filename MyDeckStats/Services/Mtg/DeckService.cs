using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;

namespace MyDeckStats.Services.Mtg
{
    public class DeckService : TrackableServiceBase<Deck, IDeckRepository>, IDeckService
    {
        public DeckService(IDeckRepository repo) : base(repo)
        {
        }
    }
}
