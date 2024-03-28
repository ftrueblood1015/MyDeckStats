using Microsoft.AspNetCore.Components;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Purposes
{
    public partial class MasterPurposeSummary : SummaryPageBase<MasterPurpose>
    {
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private string DetailRoute = "masterpurpose";

        public override Func<MasterPurpose, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        public void View(MasterPurpose purpose, string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}/{purpose.Id}", false);
            navCommand.Execute();
        }
    }
}
