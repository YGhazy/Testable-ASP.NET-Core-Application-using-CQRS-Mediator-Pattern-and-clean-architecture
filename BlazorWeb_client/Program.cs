using Blazored.LocalStorage;
using BlazorWeb_client;
using BlazorWeb_Client.Serivce;
using BlazorWeb_Client.Serivce.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWeb_client.Helper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddLocalization();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>(); //custom implementation for authProvider
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


var host = builder.Build();

var logger = host.Services.GetRequiredService<ILoggerFactory>()
	.CreateLogger<Program>();
logger.LogInformation("Logged after the app is built in Program.cs."); //in browser console

await host.SetDefaultUICulture();//CultureExtensions 
await builder.Build().RunAsync();
