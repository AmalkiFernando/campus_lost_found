namespace CampusLostFound.Api.Data;

// Minimal .env support for local dev so DATABASE_URL doesn't have to be
// exported by hand every session. No-op if DATABASE_URL is already set
// (e.g. in real deployment environments) or no .env file exists.
public static class DotEnvLoader
{
    public static void LoadIfPresent(string? directory = null)
    {
        if (Environment.GetEnvironmentVariable("DATABASE_URL") is not null)
        {
            return;
        }

        var envPath = Path.Combine(directory ?? Directory.GetCurrentDirectory(), ".env");
        if (!File.Exists(envPath))
        {
            return;
        }

        foreach (var line in File.ReadAllLines(envPath))
        {
            var trimmed = line.Trim();
            if (trimmed.Length == 0 || trimmed.StartsWith('#'))
            {
                continue;
            }

            var separatorIndex = trimmed.IndexOf('=');
            if (separatorIndex <= 0)
            {
                continue;
            }

            var key = trimmed[..separatorIndex].Trim();
            var value = trimmed[(separatorIndex + 1)..].Trim().Trim('"');
            Environment.SetEnvironmentVariable(key, value);
        }
    }
}
