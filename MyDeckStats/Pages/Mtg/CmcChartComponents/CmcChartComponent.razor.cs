using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Domain.Models;

namespace MyDeckStats.Pages.Mtg.CmcChartComponents
{
    public partial class CmcChartComponent
    {
        [Inject]
        private IDeckCardService? DeckCardService { get; set; }

        [Parameter]
        public string? Username { get; set; }

        [Parameter]
        public Guid DeckId { get; set; }

        public CmcChart? CmcChart { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            CmcChart = new CmcChart();
            GetChartData();
        }

        public void GetChartData()
        {
            if (DeckCardService == null)
            {
                throw new Exception($"{nameof(DeckCardService)}  is null!");
            }

            if (DeckId != Guid.Empty)
            {
                var Response = DeckCardService.GetCmCChartData(DeckId, Username);
                CmcChart = Response != null ? Response : new CmcChart();
                StateHasChanged();
            }
        }
    }
}
