using Microsoft.AspNetCore.Components;

namespace MyDeckStats.Commands.Navigation
{
    public class NavigationCommand : INavigationCommand
    {
        private readonly  NavigationManager NavigationManager;
        private readonly string Route;
        private readonly bool ForceReload;

        public NavigationCommand(NavigationManager navigationManager, String route, bool forceReload)
        {
            NavigationManager = navigationManager;
            Route = route;
            ForceReload = forceReload;
        }

        public void Execute()
        {
            NavigationManager.NavigateTo($"{Route}", ForceReload);
        }
    }
}
