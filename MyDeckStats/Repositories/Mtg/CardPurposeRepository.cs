using Microsoft.EntityFrameworkCore;
using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class CardPurposeRepository : RepositoryBase<CardPurpose, ApplicationDbContext>, ICardPurposeRepository
    {
        public CardPurposeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
