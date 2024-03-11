using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Interfaces.Services.Users;

namespace MyDeckStats.Pages.Users
{
    public partial class RoleDetail
    {
        [Parameter]
        public string EntityId { get; set; }

        [Inject]
        IRoleService<IdentityRole>? Service { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        public bool success;

        public MudForm? Form;

        public IdentityRole? Entity { get; set; } = new IdentityRole();

        protected override async Task OnInitializedAsync()
        {
            Entity = await SetEntity();
        }

        public async Task<IdentityRole> SetEntity()
        {
            if (EntityId.IsNullOrEmpty())
            {
                return new IdentityRole();
            }
            else
            {
                return await GetEntity(EntityId);
            }
        }

        public async void Save(IdentityRole Entity, string route)
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

            if (EntityId.IsNullOrEmpty())
            {
                Create(Entity);
            }
            else
            {
                Update(Entity);
            }

            Cancel(route);
        }

        public virtual void Create(IdentityRole Entity)
        {
            try
            {
                Entity.Id = Entity.Name!;
                Entity.NormalizedName = Entity.Name!.ToUpper();
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

        private void Update(IdentityRole Entity)
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

        public async Task<IdentityRole> GetEntity(string Id)
        {
            if (Service == null)
            {
                throw new ArgumentNullException(nameof(Service));
            }

            return Service.GetById(Id) ?? new IdentityRole() { };
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
