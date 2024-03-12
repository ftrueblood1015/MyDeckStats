using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyDeckStats.Areas.Identity;
using MyDeckStats.Data;
using MudBlazor;
using MudBlazor.Services;
using NetCore.AutoRegisterDi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

InjectPatternFromAssemblies(builder, "Repository");
InjectPatternFromAssemblies(builder, "Service");

var app = builder.Build();

await EnsureDbIsMigrated(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

async Task EnsureDbIsMigrated(IServiceProvider services)
{
    using var scope = services.CreateScope();
    using var ctx = scope.ServiceProvider.GetService<ApplicationDbContext>();
    if (ctx != null)
    {
        await ctx.Database.MigrateAsync();
    }
}

void InjectPatternFromAssemblies(WebApplicationBuilder builder, string pattern, params Assembly[] assembly)
{
    builder.Services.RegisterAssemblyPublicNonGenericClasses(GetAssemblies("MyDeckStats"))
            .Where(c => c.Name.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase))
            .AsPublicImplementedInterfaces();
}

static Assembly[] GetAssemblies(string AppName)
{
    return AppDomain.CurrentDomain.GetAssemblies()
        .Where(x => (x.FullName ?? string.Empty)
        .StartsWith(AppName, StringComparison.CurrentCultureIgnoreCase))
        .ToArray();
}
