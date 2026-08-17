using System.Text.Json.Serialization;
using CampusLostFound.Api.Data;
using Microsoft.EntityFrameworkCore;

DotEnvLoader.LoadIfPresent();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS: allowed origins are configured, not hardcoded, so the frontend's dev URL
// (e.g. Vite on 5173, CRA on 3000) can be changed without a code change.
const string FrontendCorsPolicy = "FrontendCorsPolicy";
var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy.WithOrigins(corsOrigins).AllowAnyHeader().AllowAnyMethod();
    });
});

// Database connection: prefer the DATABASE_URL env var (Neon-style postgres:// URL),
// falling back to ConnectionStrings:DefaultConnection (e.g. from user-secrets in dev).
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
var connectionString = !string.IsNullOrWhiteSpace(databaseUrl)
    ? ConnectionStringHelper.FromDatabaseUrl(databaseUrl)
    : builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "No database connection string configured. Set the DATABASE_URL environment variable " +
        "or ConnectionStrings:DefaultConnection in configuration/user-secrets.");
}

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Cloud hosts like Railway assign a dynamic port via the PORT env var and route
// external traffic to it; bind Kestrel there when present, otherwise leave
// launchSettings.json / defaults in charge (local dev).
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrWhiteSpace(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    // Avoid leaking exception details/stack traces/connection info to clients.
    app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { message = "An unexpected error occurred." });
    }));
}

app.UseHttpsRedirection();

app.UseCors(FrontendCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
