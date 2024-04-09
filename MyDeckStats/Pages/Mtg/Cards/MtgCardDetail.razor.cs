using Microsoft.AspNetCore.Components;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Cards
{
    public partial class MtgCardDetail : DetailPageBase<MtgCard>
    {
        [Parameter]
        public string EntityId { get; set; }

        [Inject]
        private IMtgKeywordService? MtgKeywordService { get; set; }

        [Inject]
        private IColorIdentityService? ColorIdentityService { get; set; }

        [Inject]
        private ICardTypeService? CardTypeService { get; set; }

        [Inject]
        private ICardPurposeService? CardPurposeService { get; set; }

        public MtgCard? Entity { get; set; } = new MtgCard();

        private List<MtgKeyword>? MtgKeywords { get; set; }

        private List<ColorIdentity>? ColorIdentities { get; set; }

        private List<CardType>? CardTypes { get; set; }

        private List<CardPurpose>? CardPurposes { get; set; }

        private bool IsReadOnly = true;

        protected override async Task OnInitializedAsync()
        {
            Entity = await SetEntity();
            await GetCardKeywords();
            await GetColorIdentities();
            await GetCardTypes();
            await GetCardPurposes();
            await base.OnInitializedAsync();
        }

        public async Task<MtgCard> SetEntity()
        {
            if (EntityId == null)
            {
                return new MtgCard();
            }
            else
            {
                return await base.GetEntity(new Guid(EntityId));
            }
        }

        private async Task GetCardKeywords()
        {
            if (MtgKeywordService == null)
            {
                throw new Exception($"{nameof(MtgKeywordService)}  is null!");
            }

            var Response = MtgKeywordService.Filter(x => x.MtgCardId == new Guid(EntityId));
            MtgKeywords = Response != null ? Response.ToList() : new List<MtgKeyword>();
        }

        private async Task GetColorIdentities()
        {
            if (ColorIdentityService == null)
            {
                throw new Exception($"{nameof(ColorIdentityService)}  is null!");
            }

            var Response = ColorIdentityService.Filter(x => x.MtgCardId == new Guid(EntityId));
            ColorIdentities = Response != null ? Response.ToList() : new List<ColorIdentity>();
        }

        private async Task GetCardTypes()
        {
            if (CardTypeService == null)
            {
                throw new Exception($"{nameof(CardTypeService)}  is null!");
            }

            var Response = CardTypeService.Filter(x => x.MtgCardId == new Guid(EntityId));
            CardTypes = Response != null ? Response.ToList() : new List<CardType>();
        }

        private async Task GetCardPurposes()
        {
            if (CardPurposeService == null)
            {
                throw new Exception($"{nameof(CardPurposeService)}  is null!");
            }

            var Response = CardPurposeService.Filter(x => x.MtgCardId == new Guid(EntityId));
            CardPurposes = Response != null ? Response.ToList() : new List<CardPurpose>();
        }
    }
}
