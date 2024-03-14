using Microsoft.AspNetCore.Components;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Sets
{
    public partial class MtgSetDetail : DetailPageBase<MtgSet>
    {
        [Parameter]
        public string EntityId { get; set; }

        public MtgSet? Entity { get; set; } = new MtgSet();

        private bool IsReadOnly = true;

        protected override async Task OnInitializedAsync()
        {
            Entity = await SetEntity();
        }

        public async Task<MtgSet> SetEntity()
        {
            if (EntityId == null)
            {
                return new MtgSet();
            }
            else
            {
                return await base.GetEntity(new Guid(EntityId));
            }
        }
    }
}
