using Microsoft.AspNetCore.Components;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Scryfall;
using MyDeckStats.Domain.Models;

namespace MyDeckStats.Pages.Mtg.Cards
{
    public partial class CardAdmin
    {
        [Inject]
        private IScryfallMtgCardService<ScryfallMtgCard>? ScryfallMtgCardService { get; set; }

        [Inject]
        private ICardProcessingService CardProcessingService { get; set; }

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
        }

        private async Task ProcessKeywords()
        {
            Disabled = true;

            if (CardProcessingService == null)
            {
                Disabled = false;
                throw new Exception($"{nameof(CardProcessingService)} is null");
            }

            var result = CardProcessingService.ProcessCardKeywords();
        }

        private async Task ProcessColorIdentities()
        {
            Disabled = true;

            if (CardProcessingService == null)
            {
                Disabled = false;
                throw new Exception($"{nameof(CardProcessingService)} is null");
            }

            var result = CardProcessingService.ProcessCardColorIdentities();
        }
    }
}
