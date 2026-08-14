using Microsoft.AspNetCore.WebUtilities;
using Npgsql;

namespace CampusLostFound.Api.Data;

public static class ConnectionStringHelper
{
    public static string FromDatabaseUrl(string databaseUrl)
    {
        var uri = new Uri(databaseUrl);
        var userInfoParts = uri.UserInfo.Split(':', 2);
        var username = Uri.UnescapeDataString(userInfoParts[0]);
        var password = userInfoParts.Length > 1 ? Uri.UnescapeDataString(userInfoParts[1]) : string.Empty;
        var database = uri.AbsolutePath.TrimStart('/');

        var queryParams = QueryHelpers.ParseQuery(uri.Query);
        var sslModeValue = queryParams.TryGetValue("sslmode", out var v) ? v.ToString() : "Require";
        if (!Enum.TryParse<SslMode>(sslModeValue, ignoreCase: true, out var sslMode))
        {
            sslMode = SslMode.Require;
        }

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port > 0 ? uri.Port : 5432,
            Database = database,
            Username = username,
            Password = password,
            SslMode = sslMode
        };

        return builder.ConnectionString;
    }
}
