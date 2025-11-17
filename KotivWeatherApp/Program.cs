using Business.Repositories;
using Business.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC/Pages + Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options =>
{
    options.DetailedErrors = true;
});

// config
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
var googleApiKey = builder.Configuration["GoogleMaps:ApiKey"] ?? "";

// repositories
builder.Services.AddScoped<IErrorLogRepository>(sp => new ErrorLogRepository(connectionString));
builder.Services.AddScoped<IApiLogRepository>(sp => new ApiLogRepository(connectionString));
builder.Services.AddScoped<ISearchHistoryRepository>(sp => new SearchHistoryRepository(connectionString));

// services
builder.Services.AddScoped<LoggingService>();
builder.Services.AddScoped(sp => new WeatherService(new HttpClient(), sp.GetRequiredService<LoggingService>()));
builder.Services.AddScoped(sp => new GoogleMapsService(new HttpClient(), sp.GetRequiredService<LoggingService>(), googleApiKey));
builder.Services.AddScoped(sp => new SearchHistoryService(connectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host"); // this is the Blazor host page

app.Run();
