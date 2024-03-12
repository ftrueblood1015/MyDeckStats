using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Interfaces.Services.Users;

namespace MyDeckStats.Pages.Users
{
    public partial class RoleSummary
    {
        [Inject]
        private IRoleService<IdentityRole> RoleService { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private string DetailRoute = "roledetail";

        public List<IdentityRole>? Entities { get; set; }

        public string? _searchString;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await GetData();
        }

        public async Task GetData()
        {
            if (RoleService == null)
            {
                throw new Exception($"{nameof(RoleService)}  is null!");
            }

            var Response = RoleService.GetAll();
            Entities = Response != null ? Response.ToList() : new List<IdentityRole>();
            StateHasChanged();
        }

        public virtual Func<IdentityRole, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.NormalizedName!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private async Task OnDeleteClicked(IdentityRole role)
        {
            if (RoleService == null)
            {
                throw new Exception($"{nameof(RoleService)}  is null!");
            }

            try
            {
                RoleService.Delete(role);
            }
            catch (Exception ex)
            {
                ShowSnackbarMessage($"Failed to delete {role.Name}", Color.Error);
            }
            await GetData();
            StateHasChanged();
        }

        public void Edit(IdentityRole role, string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}/{role.Id}", false);
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
