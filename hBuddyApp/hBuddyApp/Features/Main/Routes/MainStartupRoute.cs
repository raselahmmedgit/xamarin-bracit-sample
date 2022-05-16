using System;
using System.Threading.Tasks;

using Prism.Navigation;

namespace hBuddyApp.Features.Main.Routes
{
    public class MainStartupRoute : IMainStartupRoute
    {
        public async Task ExecuteAsync(INavigationService navigationService)
        {
            var url = $"/{nameof(Features.Shell.ShellPage)}/{nameof(Features.Main.MainPage)}";
            var r = await navigationService.NavigateAsync(url);
        }
    }
}
