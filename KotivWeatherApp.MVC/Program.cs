using Business.Repositories;
using Business.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DefaultConnection is missing from appsettings.json!");

var googleApiKey = builder.Configuration["GoogleMaps:ApiKey"]
    ?? throw new InvalidOperationException("GoogleMaps:ApiKey is missing from appsettings.json!");

builder.Services.AddScoped<IErrorLogRepository>(sp => new ErrorLogRepository(connectionString));
builder.Services.AddScoped<IApiLogRepository>(sp => new ApiLogRepository(connectionString));
builder.Services.AddScoped<ISearchHistoryRepository>(sp => new SearchHistoryRepository(connectionString));

builder.Services.AddScoped<LoggingService>();
builder.Services.AddScoped(sp => new WeatherService(new HttpClient(), sp.GetRequiredService<LoggingService>()));
builder.Services.AddScoped(sp => new GoogleMapsService(new HttpClient(), sp.GetRequiredService<LoggingService>(), googleApiKey));
builder.Services.AddScoped(sp => new SearchHistoryService(connectionString));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
