using Microsoft.AspNetCore.Components;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Cards
{
    public partial class MtgCardDetail : DetailPageBase<MtgCard>
    {
        [Parameter]
        public string EntityId { get; set; }

        public MtgCard? Entity { get; set; } = new MtgCard();

        private bool IsReadOnly = true;

        protected override async Task OnInitializedAsync()
        {
            Entity = await SetEntity();
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
    }
}
