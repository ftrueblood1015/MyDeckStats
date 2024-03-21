using Microsoft.IdentityModel.Tokens;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Services.Mtg;

namespace MyDeckStats.Services.Mtg
{
    public class CardProcessingService : ICardProcessingService
    {
        private readonly IMtgCardService CardService;
        private readonly IMtgKeywordService KeywordService;
        private readonly IColorIdentityService ColorIdentityService;

        public CardProcessingService(IMtgCardService mtgCardService, IMtgKeywordService keywordService, IColorIdentityService colorIdentityService)
        {
            CardService = mtgCardService;
            KeywordService = keywordService;
            ColorIdentityService = colorIdentityService;
        }

        public bool ProcessCardColorIdentities()
        {
            try
            {
                foreach (var card in CardService.Filter(x => x.ColorIdentity != null).ToList())
                {
                    ColorIdentityService.Filter(x => x.MtgCardId == card.Id).ToList().ForEach(x => ColorIdentityService.Delete(x));

                    card.ColorIdentity!.Split(",").Where(x => !x.IsNullOrEmpty()).Distinct().ToList().ForEach(x => ColorIdentityService.Add(new ColorIdentity() { Id = Guid.NewGuid(), Name = x.ToUpper(), Description = x, MtgCardId = card.Id }));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not process color identities: {ex}");
            }
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
