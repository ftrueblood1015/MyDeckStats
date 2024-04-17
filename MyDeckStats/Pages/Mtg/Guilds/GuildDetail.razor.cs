using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Guilds
{
    public partial class GuildDetail : DetailPageBase<Guild>
    {
        [Parameter]
        public string? EntityId { get; set; }

        public Guild? Entity { get; set; } = new Guild();

        protected override async Task OnInitializedAsync()
        {
            Entity = await SetEntity();
        }

        public async Task<Guild> SetEntity()
        {
            if (EntityId.IsNullOrEmpty())
            {
                return new Guild();
            }
            else
            {
                return await base.GetEntity(new Guid(EntityId));
            }
        }
    }
}
