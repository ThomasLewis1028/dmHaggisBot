using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MudBlazor.Services;
using SWNBlazorApp.Areas.Identity;
using SWNBlazorApp.Data;
using SWNUniverseGenerator.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services
    .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddMudServices();
builder.Services.AddSingleton<CharacterService>();
builder.Services.AddSingleton<CreationService>();
builder.Services.AddSingleton<UniverseService>();
builder.Services.AddSingleton<Universe>();
builder.Services.AddSingleton<Persistence>();
builder.Services.AddSingleton<SerializeClass>();
builder.Services.AddScoped<IMenuService, MenuService>();

var app = builder.Build();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseStaticFiles();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (!File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data/persistence.json"))
{
    new SerializeClass().SerializeData(new Persistence());
}

if (!Directory.Exists("wwwroot\\images\\starmaps"))
{
    Directory.CreateDirectory("wwwroot\\images\\starmaps");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();