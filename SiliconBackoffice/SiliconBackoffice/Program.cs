using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiliconBackoffice.Client.Pages;
using SiliconBackoffice.Components;
using SiliconBackoffice.Components.Account;
using SiliconBackoffice.Data;
using SiliconBackoffice.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddSingleton<ILogger<ServiceBusHandler>>(provider =>
    provider.GetRequiredService<ILoggerFactory>().CreateLogger<ServiceBusHandler>());

builder.Services.AddSingleton<ServiceBusHandler>(provider =>
    new ServiceBusHandler(
        provider.GetRequiredService<ILogger<ServiceBusHandler>>(),
        builder.Configuration.GetConnectionString("ServiceBus"),
        builder.Configuration["ServiceBus:courseprovider"],
        builder.Configuration["ServiceBus:BackofficeApp"],
        builder.Configuration["ServiceBus:FrontEndApp"]
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SiliconBackoffice.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// Create a new scope
using (var scope = app.Services.CreateScope())
{
    // Get the instance of ServiceBusHandler from the service provider
    var serviceBusHandler = scope.ServiceProvider.GetRequiredService<ServiceBusHandler>();

    // Start listening for messages
    serviceBusHandler.StartAsync(CancellationToken.None).GetAwaiter().GetResult();
}

app.Run();