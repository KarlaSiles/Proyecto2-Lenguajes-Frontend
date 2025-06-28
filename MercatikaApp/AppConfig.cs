using Microsoft.Extensions.Configuration;
using System.IO;

namespace MercatikaApp
{
    public static class AppConfig
    {
        public static IConfiguration Configuration { get; }

        static AppConfig()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string GetApiBaseUrl()
        {
            // Lee el valor desde la sección ApiSettings del JSON
            return Configuration["ApiSettings:BaseUrl"];
        }
    }
}
