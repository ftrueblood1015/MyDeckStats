using Microsoft.AspNetCore.Components;
using MyDeckStats.Domain.Interfaces.Services.Scryfall;
using MyDeckStats.Domain.Models;

namespace MyDeckStats.Pages.Mtg.Cards
{
    public partial class CardAdmin
    {
        [Inject]
        private IScryfallMtgCardService<ScryfallMtgCard>? ScryfallMtgCardService { get; set; }

        private bool Disabled = false;

        private async Task UpdateScryfallAll()
        {
            Disabled = true;

            if (ScryfallMtgCardService == null)
            {
                Disabled = false;
                throw new Exception($"{nameof(ScryfallMtgCardService)} is null");
            }

            var result = ScryfallMtgCardService.DownloadFileAndImport();

            if (result.IsCompletedSuccessfully)
            {
                Disabled = false;
            }

            Disabled = false;
        }
    }
}
