using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Users;

namespace MyDeckStats.Services.Mtg
{
    public class CardTypeService : ServiceBase<CardType, ICardTypeRepository>, ICardTypeService
    {
        public CardTypeService(ICardTypeRepository repo) : base(repo)
        {
        }
    }
}
