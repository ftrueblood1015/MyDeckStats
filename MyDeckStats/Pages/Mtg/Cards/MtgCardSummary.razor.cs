using Microsoft.AspNetCore.Components;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Services.Scryfall;
using MyDeckStats.Domain.Models;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Cards
{
    public partial class MtgCardSummary : SummaryPageBase<MtgCard>
    {
        [Inject]
        private IScryfallMtgCardService<ScryfallMtgCard>? ScryfallMtgCardService { get; set; }

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

        private async Task UpdateScryfallAll()
        {
            if (ScryfallMtgCardService == null)
            {
                throw new Exception($"{nameof(ScryfallMtgCardService)} is null");
            }

            var result = ScryfallMtgCardService.ImportFromFile();

            if (result.IsCompletedSuccessfully)
            {
                await GetData();
            }
        }

        public void View(MtgCard card, string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}/{card.Id}", false);
            navCommand.Execute();
        }
    }
}
