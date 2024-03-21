using Microsoft.AspNetCore.Components;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Types
{
    public partial class TypeSummary : SummaryPageBase<MasterType>
    {
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private string DetailRoute = "mastertype";

        public override Func<MasterType, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        public void View(MtgSet set, string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}/{set.Id}", false);
            navCommand.Execute();
        }
    }
}
