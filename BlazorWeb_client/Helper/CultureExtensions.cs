using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;

namespace BlazorWeb_client.Helper
{
    public static class CultureExtensions
    {
        public async static Task SetDefaultUICulture(this WebAssemblyHost host)
        {

            var localStorage = host.Services.GetRequiredService<ILocalStorageService>();

            var result = await localStorage.GetItemAsync<string>("currentcuture");
            CultureInfo culture;
            if (result != null)
                culture = new CultureInfo(result);
            else
                culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
