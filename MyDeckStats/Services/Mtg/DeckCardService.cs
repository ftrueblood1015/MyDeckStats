using MudBlazor;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Domain.Models;

namespace MyDeckStats.Services.Mtg
{
    public class DeckCardService : TrackableServiceBase<DeckCard, IDeckCardRepository>, IDeckCardService
    {
        public DeckCardService(IDeckCardRepository repo) : base(repo)
        {
        }

        public CmcChart GetCmCChartData(Guid DeckId, string username)
        {
            var result = new CmcChart();
            result.Series = new List<ChartSeries>();
            result.Options = new ChartOptions();

            var labels = new List<string>();
            var data = new List<double>();

            var deckCards = Filter(x => x.DeckId == DeckId, username);
            deckCards = deckCards.Where(c => !c.MtgCard!.Type!.Contains("Basic Land"));

            int minCmc = deckCards.Min(x => x.MtgCard!.ConvertedManaCost);
            int maxCmc = deckCards.Max(x => x.MtgCard!.ConvertedManaCost);

            var cmcs = Enumerable.Range(minCmc, maxCmc).Select(x => x).ToList();

            foreach (var cmc in cmcs)
            {
                var count = deckCards.Where(x => x.MtgCard!.ConvertedManaCost == cmc).Count();
                labels.Add(cmc.ToString());
                data.Add(count);
            }

            result.Series!.Add(new ChartSeries() { Name = "Mana Cost Curve", Data = data.ToArray() });

            result.XAxisLabels = labels.ToArray();

            result.Options!.InterpolationOption = InterpolationOption.Straight;
            result.Options.YAxisTicks = 1;
            result.Options.LineStrokeWidth = 5;
            result.Options.XAxisLines = true;

            result.ChartType = ChartType.Line;

            return result;
        }
    }
}
