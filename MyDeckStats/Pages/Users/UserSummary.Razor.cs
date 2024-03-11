using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using MyDeckStats.Domain.Interfaces.Services.Users;

namespace MyDeckStats.Pages.Users
{
    public partial class UserSummary
    {
        [Inject]
        private IUserService<IdentityUser> UserService { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        public List<IdentityUser>? Entities { get; set; }

        public string? _searchString;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await GetData();
        }

        public async Task GetData()
        {
            if (UserService == null)
            {
                throw new Exception($"{nameof(UserService)}  is null!");
            }

            var Response = UserService.GetAll();
            Entities = Response != null ? Response.ToList() : new List<IdentityUser>();
            StateHasChanged();
        }

        public virtual Func<IdentityUser, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.UserName!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Email!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private async Task Lock(IdentityUser user)
        {
            user.LockoutEnabled = !user.LockoutEnabled;
            if (user.LockoutEnabled)
            {
                user.LockoutEnd = DateTime.Now.AddDays(1);
            }
            else
            {
                user.LockoutEnd = DateTime.Now;
            }

            user.LockoutEnd = DateTime.Now.AddDays(1);
            await Update(user);
        }

        private async Task Update(IdentityUser user)
        {
            if (UserService == null)
            {
                throw new Exception($"{nameof(UserService)}  is null!");
            }

            try
            {
                UserService.Update(user);
            }
            catch (Exception ex)
            {
                ShowSnackbarMessage($"Failed to update {user.UserName}", Color.Error);
            }
            await GetData();
            StateHasChanged();
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
