using Microsoft.AspNetCore.Components;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Decks
{
    public partial class DeckSummary : TrackableSummaryPageBase<Deck>
    {
        private string DetailRoute = "deck";

        public override Func<Deck, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };
    }
}
