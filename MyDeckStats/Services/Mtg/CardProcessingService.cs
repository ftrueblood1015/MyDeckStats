using Microsoft.IdentityModel.Tokens;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using System.Linq;

namespace MyDeckStats.Services.Mtg
{
    public class CardProcessingService : ICardProcessingService
    {
        private readonly IMtgCardService CardService;
        private readonly IMtgKeywordService KeywordService;
        private readonly IColorIdentityService ColorIdentityService;
        private readonly ICardTypeService CardTypeService;
        private readonly IMasterTypeService MasterTypeService;

        public CardProcessingService(
            IMtgCardService mtgCardService,
            IMtgKeywordService keywordService,
            IColorIdentityService colorIdentityService,
            ICardTypeService cardTypeService,
            IMasterTypeService masterTypeService
            )
        {
            CardService = mtgCardService;
            KeywordService = keywordService;
            ColorIdentityService = colorIdentityService;
            CardTypeService = cardTypeService;
            MasterTypeService = masterTypeService;
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

        public bool ProcessCardTypes()
        {
            try
            {
                var allTypes = MasterTypeService.GetAll().ToList();

                foreach (var card in CardService.Filter(x => x.Type != null).ToList())
                {
                    CardTypeService.Filter(x => x.MtgCardId == card.Id).ToList().ForEach(x => CardTypeService.Delete(x));

                    foreach (var type in allTypes)
                    {
                        if (card.Type!.ToUpper().Contains(type.Name!.ToUpper()))
                        {
                            CardTypeService.Add(new CardType() { Id = Guid.NewGuid(), Name = type.Name, Description = $"{card.Name} - {type.Name}", MtgCardId = card.Id });
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not process card types: {ex}");
            }
        }
    }
}
