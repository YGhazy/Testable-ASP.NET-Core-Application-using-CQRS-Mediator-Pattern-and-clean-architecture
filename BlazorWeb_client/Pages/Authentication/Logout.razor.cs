using BlazorWeb_Client.Serivce.IService;
using Microsoft.AspNetCore.Components;

namespace BlazorWeb_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService _authSerivce { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

	}
}
