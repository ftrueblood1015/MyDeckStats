using Microsoft.AspNetCore.Components;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Formats
{
    public partial class FormatSummary : SummaryPageBase<Format>
    {
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private string DetailRoute = "format";

        public override Func<Format, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        public void View(Format format, string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}/{format.Id}", false);
            navCommand.Execute();
        }
    }
}
