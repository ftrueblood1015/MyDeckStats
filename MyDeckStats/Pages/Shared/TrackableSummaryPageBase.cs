using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Entities;
using MyDeckStats.Domain.Interfaces.Services;

namespace MyDeckStats.Pages.Shared
{
    public class TrackableSummaryPageBase<T> : ComponentBase
        where T : TrackableEntityBase
    {
        [Inject]
        protected ITrackableServiceBase<T>? Service { get; set; }

        [Inject]
        private IDialogService? DialogService { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        [CascadingParameter] protected Task<AuthenticationState> AuthStat { get; set; }

        public List<T>? Entities { get; set; }

        public string? _searchString;

        private string State = "Message box hasn't been opened yet";

        private string? username { get; set; }

        protected override async Task OnInitializedAsync()
        {
            username = (await AuthStat).User.Identity!.Name!;
            await GetData();
        }

        public async Task GetData()
        {
            if (Service == null)
            {
                throw new Exception($"{nameof(Service)}  is null!");
            }

            var Response = Service.GetAll(username);
            Entities = Response != null ? Response.ToList() : new List<T>();
            StateHasChanged();
        }

        public virtual Func<T, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Description!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        public async void OnDeleteClicked(T item)
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
                await Delete(item);
            }
        }

        private async Task<bool> Delete(T Item)
        {
            if (Service == null)
            {
                throw new Exception($"{nameof(Service)} is null!");
            }

            try
            {
                var result = Service.Delete(Item, username);

                if (result)
                {
                    ShowSnackbarMessage($"Deleted {Item.Name}", MudBlazor.Color.Success);
                    await GetData();
                }
                else
                {
                    ShowSnackbarMessage($"Could Not Delete {Item.Name}", MudBlazor.Color.Error);
                }

                return result;
            }
            catch (Exception ex)
            {
                ShowSnackbarMessage($"Could Not Delete {Item.Name}: {ex}", MudBlazor.Color.Error);
                return false;
            }
        }

        public void Edit(T item, string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}/{item.Id}", false);
            navCommand.Execute();
        }

        public void Add(string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}", false);
            navCommand.Execute();
        }

        private void ShowSnackbarMessage(string Message, MudBlazor.Color Color)
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
    }
}
