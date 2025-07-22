using BlazorPurchaseOrders.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization; // Aseg�rate de que este using est� presente
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Agrega tu Connection String aqu�, usando el nombre correcto de tu base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuraci�n de Identity para autenticaci�n
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
     .AddRoles<IdentityRole>()// es para usar los roles 
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ****** INICIO DE LAS CORRECCIONES EN builder.Services ******
// Agrega esto para Blazor Server Authentication
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddAuthorization(); // A�ade los servicios de autorizaci�n
// ****** FIN DE LAS CORRECCIONES EN builder.Services ******

builder.Services.AddSyncfusionBlazor();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>(); // Considera si realmente lo necesitas para tu app

var sqlConnectionConfiguration = new SqlConnectionConfiguration(builder.Configuration.GetConnectionString("SqlDBContext"));
builder.Services.AddSingleton(sqlConnectionConfiguration);

builder.Services.AddScoped<IPOHeaderService, POHeaderService>();
builder.Services.AddScoped<IPOLineService, POLineService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ITaxService, TaxService>();

var app = builder.Build();

//Register Syncfusion License
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzk1MjUzMEAzMjMzMmUzMDJlMzBEdHlwbWEzem5hZ1BHOUFNbUVSNkJKT0tqbnYvN0ZpVWpaT1RHN2VIL3p3PQ==");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ****** AGREGADO PARA LA AUTENTIFICACION ******
app.UseAuthentication();
app.UseAuthorization();
// ****** FIN ******

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();