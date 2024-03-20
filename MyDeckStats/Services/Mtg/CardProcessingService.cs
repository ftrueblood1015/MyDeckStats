using Microsoft.IdentityModel.Tokens;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Services.Mtg;

namespace MyDeckStats.Services.Mtg
{
    public class CardProcessingService : ICardProcessingService
    {
        private readonly IMtgCardService CardService;
        private readonly IMtgKeywordService KeywordService;

        public CardProcessingService(IMtgCardService mtgCardService, IMtgKeywordService keywordService)
        {
            CardService = mtgCardService;
            KeywordService = keywordService;
        }

        public bool ProcessCardKeywords()
        {
            try
            {
                foreach (var card in CardService.Filter(x => x.Keywords != null).ToList())
                {
                    KeywordService.Filter(x => x.MtgCardId == card.Id).ToList().ForEach(x => KeywordService.Delete(x));

                    card.Keywords!.Split(",").Where(x => !x.IsNullOrEmpty()).Distinct().ToList().ForEach(x => KeywordService.Add(new MtgKeyword() { Id = Guid.NewGuid(), Name = x, Description = x, MtgCardId = card.Id }));
                }

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not process keywords: {ex}");
            }
        }
    }
}
