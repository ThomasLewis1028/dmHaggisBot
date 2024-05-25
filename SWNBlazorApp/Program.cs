using MudBlazor.Services;
using SWNBlazorApp.Data;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(b => b.AddConsole());
builder.Services.AddDbContext<UniverseContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// builder.Services
//     .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddMudServices();

builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddTransient<Universe>();
builder.Services.AddScoped<CharacterService>();
builder.Services.AddScoped<UniverseService>();
builder.Services.AddScoped<ZoneService>();
builder.Services.AddScoped<PlanetService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<PointOfInterestService>();
builder.Services.AddScoped<ShipService>();
builder.Services.AddScoped<StarService>();
builder.Services.AddScoped<CrewMemberService>();

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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();