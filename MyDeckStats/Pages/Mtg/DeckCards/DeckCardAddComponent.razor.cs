using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using static MudBlazor.CategoryTypes;

namespace MyDeckStats.Pages.Mtg.DeckCards
{
    public partial class DeckCardAddComponent
    {
        [Inject]
        private IDeckCardService? DeckCardService { get; set; }

        [Inject]
        private IDialogService? DialogService { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        [Parameter]
        public string? Username { get; set; }

        [Parameter]
        public Guid DeckId { get; set; }

        private DeckCard? DeckCard { get; set; }

        private string State = "Message box hasn't been opened yet";

        public List<DeckCard>? Entities { get; set; }

        public MudForm? DeckCardForm;

        public bool success {  get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            DeckCard = new DeckCard();
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
                var Response = DeckCardService.Filter(x => x.DeckId == DeckId, Username);
                Entities = Response != null ? Response.ToList() : new List<DeckCard>();
                StateHasChanged();
            }
        }

        public async void AddDeckcard()
        {
            if (DeckCardService == null)
            {
                throw new Exception($"{nameof(DeckCardService)} is null!");
            }

            try
            {
                DeckCard!.DeckId = DeckId;
                DeckCardService!.Add(DeckCard!, Username!);
                StateHasChanged();
                ShowSnackbarMessage($"Added New {DeckCard!.Name}", Color.Success);
                DeckCard = new DeckCard();
                await GetData();
            }
            catch
            {
                ShowSnackbarMessage($"Could Not Add {DeckCard!.Name}", Color.Error);
            }
        }

        public async Task<bool> RemoveDeckcard(DeckCard deckCard)
        {
            if (DeckCardService == null)
            {
                throw new Exception($"{nameof(DeckCardService)} is null!");
            }

            try
            {
                var result = DeckCardService.Delete(deckCard, Username);

                if (result)
                {
                    ShowSnackbarMessage($"Deleted {deckCard.Name}", Color.Success);
                    await GetData();
                }
                else
                {
                    ShowSnackbarMessage($"Could Not Delete {deckCard.Name}", Color.Error);
                }

                return result;
            }
            catch (Exception ex)
            {
                ShowSnackbarMessage($"Could Not Delete {deckCard.Name}: {ex}", Color.Error);
                return false;
            }
        }

        public async void OnDeleteClicked(DeckCard deckCard)
        {
            if (DialogService == null)
            {
                throw new Exception($"{nameof(DialogService)}  is null!");
            }

            bool? result = await DialogService.ShowMessageBox(
                "Warning",
                "Deleting can not be undone!",
                yesText:"Delete!", cancelText:"Cancel"
            );

            State = result == null ? "Canceled" : "Deleted!";

            if (State != "Canceled")
            {
                await RemoveDeckcard(deckCard);
            }
        }

        private void ShowSnackbarMessage(string Message, Color Color)
        {
            if (SnackbarService == null)
            {
                throw new ArgumentNullException(nameof(SnackbarService));
            }

            SnackbarService.Add<MudChip>(new Dictionary<string, object>() {
                { "Text", $"{Message}" },
                { "Color", Color }
            });
        }

        public async Task CardChange(MtgCard? card)
        {
            DeckCard!.Name = card!.Name;
            DeckCard!.Description = $"{card.Name} - {DeckId}";
            DeckCard.MtgCardId = card!.Id;
        }
    }
}
