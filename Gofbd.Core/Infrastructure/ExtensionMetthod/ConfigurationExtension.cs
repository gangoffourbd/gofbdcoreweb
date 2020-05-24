
namespace Gofbd.Core
{
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtension
    {
        public static string GetJwtSecretKey(this IConfiguration configuration)
        {
            return configuration.GetSection("Authentication:SecretKey").Value;
        }

        public static string GetJwtIssuer(this IConfiguration configuration)
        {
            return configuration.GetSection("Authentication:Issuer").Value;
        }

        public static string GetJwtAudience(this IConfiguration configuration)
        {
            return configuration.GetSection("Authentication:Audience").Value;
        }

        public static string GetAuthHeaderKey(this IConfiguration configuration)
        {
            return configuration.GetSection("Authentication:Audience").Value;
        }
    }
}