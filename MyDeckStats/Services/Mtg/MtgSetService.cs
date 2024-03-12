using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Users;

namespace MyDeckStats.Services.Mtg
{
    public class MtgSetService : ServiceBase<MtgSet, IMtgSetRepository>, IMtgSetService
    {
        public MtgSetService(IMtgSetRepository repo) : base(repo)
        {
        }

        public MtgSet? GetByScryfallId(string id)
        {
            try
            {
                return Repo.Filter(x => x.ScryfallId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
