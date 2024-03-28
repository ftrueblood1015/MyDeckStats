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
        private readonly ICardTypeService CardTypeService;
        private readonly IMasterTypeService MasterTypeService;
        private readonly IMasterPurposeService MasterPurposeService;
        private readonly ICardPurposeService CardPurposeService;

        public CardProcessingService(
            IMtgCardService mtgCardService,
            IMtgKeywordService keywordService,
            IColorIdentityService colorIdentityService,
            ICardTypeService cardTypeService,
            IMasterTypeService masterTypeService,
            IMasterPurposeService masterPurposeService,
            ICardPurposeService cardPurposeService
            )
        {
            CardService = mtgCardService;
            KeywordService = keywordService;
            ColorIdentityService = colorIdentityService;
            CardTypeService = cardTypeService;
            MasterTypeService = masterTypeService;
            MasterPurposeService = masterPurposeService;
            CardPurposeService = cardPurposeService;
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

        public bool ProcessCardPurpose(Guid CardId)
        {
            try
            {
                var MasterPurposes = MasterPurposeService.Filter(x => x.IsActive == true).ToList();
                var Card = CardService.GetById(CardId);

                CardPurposeService.Filter(x => x.MtgCardId == CardId).ToList().ForEach(x => CardPurposeService.Delete(x));

                foreach (var purpose in MasterPurposes)
                {
                    var IncludeTerms = purpose.IncludeTerms!.ToUpper().Split(",").Where(x => !x.IsNullOrEmpty());
                    var ExcludeTerms = purpose.ExcludeTerms != null ? purpose.ExcludeTerms!.ToUpper().Split(",").Where(x => !x.IsNullOrEmpty()) : new string[] { };

                    if (IncludeTerms.All(x => Card!.OracleText!.ToUpper().Contains(x)) && !ExcludeTerms.Any(x => Card!.OracleText!.ToUpper().Contains(x)))
                    {
                        CardPurposeService.Add(
                            new CardPurpose() { Id = Guid.NewGuid(), Description = $"{Card!.Name} - {purpose.Name}", Name = $"{purpose.Name}", MtgCardId = CardId }
                        );
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not process card purpose: {ex}");
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
