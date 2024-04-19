using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Models;

namespace MyDeckStats.Domain.Interfaces.Services.Mtg
{
    public interface IDeckCardService : ITrackableServiceBase<DeckCard>
    {
        CmcChart GetCmCChartData(Guid DeckId, string username);
        IEnumerable<KeywordStat> GetKeywordStatistics(Guid DeckId, string username);
    }
}
