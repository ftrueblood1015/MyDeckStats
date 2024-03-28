using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Purposes
{
    public partial class MasterPurposeDetail : DetailPageBase<MasterPurpose>
    {
        [Parameter]
        public string EntityId { get; set; }

        public MasterPurpose? Entity { get; set; } = new MasterPurpose();

        protected override async Task OnInitializedAsync()
        {
            Entity = await SetEntity();
        }

        public async Task<MasterPurpose> SetEntity()
        {
            if (EntityId.IsNullOrEmpty())
            {
                return new MasterPurpose();
            }
            else
            {
                return await base.GetEntity(new Guid(EntityId));
            }
        }
    }
}
