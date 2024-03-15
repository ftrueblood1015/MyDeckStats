using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Users;

namespace MyDeckStats.Services.Mtg
{
    public class MtgCardService : ServiceBase<MtgCard, IMtgCardRepository>, IMtgCardService
    {
        public MtgCardService(IRepositoryBase<MtgCard> repo) : base(repo)
        {
        }
    }
}
