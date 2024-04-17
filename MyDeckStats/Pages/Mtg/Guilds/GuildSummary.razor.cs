using Microsoft.AspNetCore.Components;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Guilds
{
    public partial class GuildSummary : SummaryPageBase<Guild>
    {
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private string DetailRoute = "guild";

        public override Func<Guild, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };
    }
}
