using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Types
{
    public partial class TypeDetail : DetailPageBase<MasterType>
    {
        [Parameter]
        public string EntityId { get; set; }

        public MasterType? Entity { get; set; } = new MasterType();

        protected override async Task OnInitializedAsync()
        {
            Entity = await SetEntity();
        }

        public async Task<MasterType> SetEntity()
        {
            if (EntityId.IsNullOrEmpty())
            {
                return new MasterType();
            }
            else
            {
                return await base.GetEntity(new Guid(EntityId));
            }
        }
    }
}
