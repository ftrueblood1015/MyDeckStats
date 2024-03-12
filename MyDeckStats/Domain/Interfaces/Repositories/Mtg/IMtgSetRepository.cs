using MyDeckStats.Domain.Entities.Mtg.Cards;

namespace MyDeckStats.Domain.Interfaces.Repositories.Mtg
{
    public interface IMtgSetRepository : IRepositoryBase<MtgSet>
    {
        MtgSet? GetByScryfallId(string id);
    }
}
