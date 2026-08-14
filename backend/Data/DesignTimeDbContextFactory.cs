using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CampusLostFound.Api.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        DotEnvLoader.LoadIfPresent();

        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        var connectionString = !string.IsNullOrWhiteSpace(databaseUrl)
            ? ConnectionStringHelper.FromDatabaseUrl(databaseUrl)
            : "Host=localhost;Database=campus_lost_found;Username=postgres;Password=postgres";

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
