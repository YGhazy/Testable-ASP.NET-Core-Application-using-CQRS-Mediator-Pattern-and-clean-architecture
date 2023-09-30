using Blazor.API.Helper;
using Blazor.Application.IRepository;
using Blazor.Application.Repository;
using Blazor.Domain.Entities;
using Blazor.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Blazor.Application.Services.IServices;
using Blazor.Application.Services;
using Blazor.Application.Unit_of_work;
using Blazor.Domain.Common;
using Blazor.Application.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorWeb_Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Bearer and then token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var apiSettingsSection = builder.Configuration.GetSection("APISettings");
builder.Services.Configure<APISettings>(apiSettingsSection);


var apiSettings = apiSettingsSection.Get<APISettings>();
var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters= new()
    {
        ValidateIssuerSigningKey=true,
        IssuerSigningKey= new SymmetricSecurityKey(key),
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = apiSettings.ValidAudience,
        ValidIssuer = apiSettings.ValidIssuer,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAccountService, Blazor.Application.Services.AccountService>();
builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();
builder.Services.AddScoped<IProductService, Blazor.Application.Services.ProductService>();
builder.Services.AddCors(o => o.AddPolicy("CustomCors", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();


StripeConfiguration.ApiKey=builder.Configuration.GetSection("Stripe")["ApiKey"];

// Configure the HTTP request pipeline.


app.UseSwagger();
if (!app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API v1");
        c.RoutePrefix= String.Empty;
    });
}
else
{
    app.UseSwaggerUI(c => {
    });
}

app.UseHttpsRedirection();
app.UseCors("CustomCors");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
