using Microsoft.AspNetCore.Components;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Cards
{
    public partial class MtgCardSummary : SummaryPageBase<MtgCard>
    {
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private string DetailRoute = "mtgcarddetail";

        public override Func<MtgCard, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        public void View(MtgCard card, string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}/{card.Id}", false);
            navCommand.Execute();
        }
    }
}
