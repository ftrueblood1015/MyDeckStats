using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Interfaces.Services.Users;

namespace MyDeckStats.Pages.Users
{
    public partial class UserRoleDetail
    {
        [Inject]
        IRoleService<IdentityRole>? RoleService { get; set; }

        [Inject]
        IUserService<IdentityUser>? UserService { get; set; }

        [Inject]
        IUserRoleService<IdentityUserRole<string>>? UserRoleService { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        public bool success;

        public MudForm? Form;

        public IdentityUserRole<string>? Entity { get; set; } = new IdentityUserRole<string>();

        public List<IdentityRole>? Roles { get; set; }

        public List<IdentityUser>? Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Entity = SetEntity();
            await GetRoles();
            await GetUsers();
            await base.OnInitializedAsync();
        }

        public IdentityUserRole<string> SetEntity()
        {
            return new IdentityUserRole<string>();
        }

        public async Task GetRoles()
        {
            if (RoleService == null)
            {
                throw new Exception($"{nameof(RoleService)}  is null!");
            }

            var Response = RoleService.GetAll();
            Roles = Response != null ? Response.ToList() : new List<IdentityRole>();
        }

        public async Task GetUsers()
        {
            if (UserService == null)
            {
                throw new Exception($"{nameof(UserService)}  is null!");
            }

            var Response = UserService.GetAll();
            Users = Response != null ? Response.ToList() : new List<IdentityUser>();
        }

        public virtual void Create(IdentityUserRole<string> Entity)
        {
            try
            {
                UserRoleService!.Add(Entity);
                success = true;
                StateHasChanged();
                ShowSnackbarMessage($"Added New User Role", Color.Success);
                Cancel("userroles");
            }
            catch
            {
                success = false;
                ShowSnackbarMessage($"Could Not Add User Role", Color.Error);
            }
        }

        public void Cancel(string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}", true);
            navCommand.Execute();
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
