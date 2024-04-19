using Microsoft.AspNetCore.Components;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Domain.Models;

namespace MyDeckStats.Pages.Mtg.KeywordStatComponents
{
    public partial class KeywordStatComponent
    {
        [Inject]
        private IDeckCardService? DeckCardService { get; set; }

        [Parameter]
        public string? Username { get; set; }

        [Parameter]
        public Guid DeckId { get; set; }

        public List<KeywordStat>? Entities { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Entities = new List<KeywordStat>();
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
                var Response = DeckCardService.GetKeywordStatistics(DeckId, Username);
                Entities = Response != null ? Response.ToList() : new List<KeywordStat>();
                StateHasChanged();
            }
        }
    }
}
