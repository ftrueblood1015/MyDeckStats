using Microsoft.Build.Framework;
using MudBlazor;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Domain.Models;

namespace MyDeckStats.Services.Mtg
{
    public class DeckCardService : TrackableServiceBase<DeckCard, IDeckCardRepository>, IDeckCardService
    {
        private readonly IMtgKeywordService MtgKeywordService;
        private readonly ICardPurposeService MtgCardPurposeService;

        public DeckCardService(IDeckCardRepository repo, IMtgKeywordService mtgKeywordService, ICardPurposeService mtgCardPurposeService) : base(repo)
        {
            MtgKeywordService = mtgKeywordService;
            MtgCardPurposeService = mtgCardPurposeService;
        }

        public CmcChart GetCmCChartData(Guid DeckId, string username)
        {
            var result = new CmcChart();
            result.Series = new List<ChartSeries>();
            result.Options = new ChartOptions();

            var labels = new List<string>();
            var data = new List<double>();

            var deckCards = Filter(x => x.DeckId == DeckId, username);
            deckCards = deckCards.Where(c => !c.MtgCard!.Type!.ToLower().Contains("basic land"));

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

        public IEnumerable<KeywordStat> GetKeywordStatistics(Guid DeckId, string username)
        {
            var results = new List<KeywordStat>();

            var deckCards = Filter(x => x.DeckId == DeckId, username).ToList();

            foreach (var card in deckCards)
            {
                var keyWords = MtgKeywordService.Filter(x => x.MtgCardId == card.MtgCardId).ToList();

                foreach (var keyWord in keyWords)
                {
                    bool exists = results.Where(x => x.Keyword == keyWord.Name).Any();
                    if (exists)
                    {
                        var existing = results.Find(x => x.Keyword == keyWord.Name);
                        if (existing != null)
                        {
                            existing.Count += 1;
                        }
                    }
                    else
                    {
                        results.Add(new KeywordStat() { Keyword = keyWord.Name, Count = 1 });
                    }
                }
            }

            return results;
        }

        public IEnumerable<PurposeStat> GetPurposeStatistics(Guid DeckId, string username)
        {
            var results = new List<PurposeStat>();

            var deckCards = Filter(x => x.DeckId == DeckId, username).ToList();

            foreach (var card in deckCards)
            {
                var purposes = MtgCardPurposeService.Filter(x => x.MtgCardId == card.MtgCardId).ToList();

                foreach (var purpose in purposes)
                {
                    bool exists = results.Where(x => x.Purpose == purpose.Name).Any();
                    if (exists)
                    {
                        var existing = results.Find(x => x.Purpose == purpose.Name);
                        if (existing != null)
                        {
                            existing.Count += 1;
                        }
                    }
                    else
                    {
                        results.Add(new PurposeStat() { Purpose = purpose.Name, Count = 1 });
                    }
                }
            }

            return results;
        }
    }
}
