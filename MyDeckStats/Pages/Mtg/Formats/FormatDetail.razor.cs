using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Formats
{
    public partial class FormatDetail : DetailPageBase<Format>
    {
        [Parameter]
        public string? EntityId { get; set; }

        public Format? Entity { get; set; } = new Format();

        protected override async Task OnInitializedAsync()
        {
            Entity = await SetEntity();
        }

        public async Task<Format> SetEntity()
        {
            if (EntityId.IsNullOrEmpty())
            {
                return new Format();
            }
            else
            {
                return await base.GetEntity(new Guid(EntityId));
            }
        }
    }
}
