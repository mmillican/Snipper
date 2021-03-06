using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Snipper.Web
{
    public static class ExtensionMethods
    {
        public static string GetDataProvider(this IConfiguration configuration)
             => configuration["DataProvider"];

        public static bool IsSqlServerDataProvider(this IConfiguration configuration)
            => GetDataProvider(configuration).ToLower() == "sqlserver";

        public static string Slugify(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var regex = new Regex("[^a-z0-9\\-_]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            input = input.Replace(" ", "-");
            var cleaned = regex.Replace(input, "").ToLower();

            while (cleaned.Contains("--"))
            {
                cleaned = cleaned.Replace("--", "-");
            }

            return cleaned;
        }
    }
}
