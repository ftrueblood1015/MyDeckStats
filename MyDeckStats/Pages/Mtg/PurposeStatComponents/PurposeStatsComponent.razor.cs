using Microsoft.AspNetCore.Components;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Domain.Models;

namespace MyDeckStats.Pages.Mtg.PurposeStatComponents
{
    public partial class PurposeStatsComponent
    {
        [Inject]
        private IDeckCardService? DeckCardService { get; set; }

        [Parameter]
        public string? Username { get; set; }

        [Parameter]
        public Guid DeckId { get; set; }

        public List<PurposeStat>? Entities { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Entities = new List<PurposeStat>();
            await GetData();
        }

        public async Task GetData()
        {
            if (DeckCardService == null)
            {
                throw new Exception($"{nameof(DeckCardService)}  is null!");
            }

            if (DeckId != Guid.Empty)
            {
                var Response = DeckCardService.GetPurposeStatistics(DeckId, Username);
                Entities = Response != null ? Response.ToList() : new List<PurposeStat>();
                StateHasChanged();
            }
        }
    }
}
