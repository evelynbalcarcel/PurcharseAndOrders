//Program.cs
using BlazorPurchaseOrders.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Configuration;
using Syncfusion.Blazor;


var builder = WebApplication.CreateBuilder(args);

// Agrega tu Connection String aquí, usando el nombre correcto de tu base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuración de Identity para autenticación
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSyncfusionBlazor(); // Asegúrate de que tienes el paquete NuGet Syncfusion.Blazor instalado

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
