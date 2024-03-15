using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class CardTypeRepository : RepositoryBase<CardType, ApplicationDbContext>, ICardTypeRepository
    {
        public CardTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
