using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Decks
{
    public partial class DeckDetail : TrackableDetailPageBase<Deck>
    {
        [CascadingParameter] protected Task<AuthenticationState> AuthStat { get; set; }

        [Parameter]
        public string? EntityId { get; set; }

        public Deck? Entity { get; set; } = new Deck();

        protected override async Task OnInitializedAsync()
        {
            username = (await AuthStat).User.Identity.Name;
            Entity = await SetEntity();
        }

        public async Task<Deck> SetEntity()
        {
            if (EntityId.IsNullOrEmpty())
            {
                return new Deck();
            }
            else
            {
                return await base.GetEntity(new Guid(EntityId));
            }
        }

        public async Task FormatChange(Format? format)
        {
            Entity.FormatId = format.Id;
        }

        public async Task GuildChange(Guild? guild)
        {
            Entity.GuildId = guild.Id;
        }

        public async Task CommanderChange(MtgCard? card)
        {
            Entity.MtgCardId = card!.Id;
        }
    }
}
