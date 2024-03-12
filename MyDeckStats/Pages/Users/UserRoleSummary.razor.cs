using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Interfaces.Services.Users;
namespace MyDeckStats.Pages.Users
{
    public partial class UserRoleSummary
    {
        [Inject]
        private IUserRoleService<IdentityUserRole<String>> Service { get; set; }

        [Inject]
        private IUserService<IdentityUser> UserService { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private string DetailRoute = "userroledetail";

        public List<IdentityUserRole<String>>? Entities { get; set; }

        public string? _searchString;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await GetData();
        }

        public async Task GetData()
        {
            if (Service == null)
            {
                throw new Exception($"{nameof(Service)}  is null!");
            }

            var Response = Service.GetAll();
            Entities = Response != null ? Response.ToList() : new List<IdentityUserRole<string>>();
            StateHasChanged();
        }

        public void Add(string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}", false);
            navCommand.Execute();
        }

        public virtual Func<IdentityUserRole<string>, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.UserId!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.RoleId!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private async Task OnDeleteClicked(IdentityUserRole<string> userRole)
        {
            if (Service == null)
            {
                throw new Exception($"{nameof(Service)}  is null!");
            }

            try
            {
                Service.Delete(userRole);
            }
            catch (Exception ex)
            {
                ShowSnackbarMessage($"Failed to delete {userRole.UserId}-{userRole.RoleId}", Color.Error);
            }
            await GetData();
            StateHasChanged();
        }

        public string GetUserById(string Id)
        {
            if (UserService == null)
            {
                throw new Exception($"{nameof(UserService)}  is null!");
            }

            var Response = UserService.GetById(Id);
            return Response != null ? Response.UserName! : String.Empty;
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
