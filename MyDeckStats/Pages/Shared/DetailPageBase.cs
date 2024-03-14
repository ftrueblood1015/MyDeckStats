using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Entities;
using MyDeckStats.Domain.Interfaces.Services;

namespace MyDeckStats.Pages.Shared
{
    public class DetailPageBase<T> : ComponentBase
        where T : EntityBase, new()
    {
        [Inject]
        IServiceBase<T>? Service { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        public bool success;

        public MudForm? Form;

        public async void Save(T Entity, string route)
        {
            success = false;
            await Form!.Validate();

            if (NavigationManager == null)
            {
                return;
            }

            if (!Form!.IsValid)
            {
                ShowSnackbarMessage($"Form Is Invalid, see errors", Color.Error);
                return;
            }

            if (Service == null)
            {
                throw new ArgumentNullException(nameof(Service));
            }

            if (Entity.Id.ToString() == null)
            {
                Create(Entity);
            }
            else
            {
                Update(Entity);
            }

            Cancel(route);
        }

        public virtual void Create(T Entity)
        {
            try
            {
                Service!.Add(Entity);
                success = true;
                StateHasChanged();
                ShowSnackbarMessage($"Added New {Entity.Name}", Color.Success);
            }
            catch
            {
                success = false;
                ShowSnackbarMessage($"Could Not Add {Entity.Name}", Color.Error);
            }
        }

        private void Update(T Entity)
        {
            try
            {
                Service!.Update(Entity);
                success = true;
                StateHasChanged();
                ShowSnackbarMessage($"Updated New {Entity.Name}", Color.Success);
            }
            catch
            {
                success = false;
                ShowSnackbarMessage($"Could Not Update {Entity.Name}", Color.Error);
            }
        }

        public void Cancel(string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}", true);
            navCommand.Execute();
        }

        public async Task<T> GetEntity(Guid Id)
        {
            if (Service == null)
            {
                throw new ArgumentNullException(nameof(Service));
            }

            return Service.GetById(Id) ?? new T() { };
        }

        public void ShowSnackbarMessage(string Message, Color Color)
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
