using Microsoft.Extensions.Configuration;

namespace TechnicalTest_NetForemost.Data.Repositories
{
    namespace TechnicalTest_NetForemost.Data
    {
        public class AppSettings
        {
            public static IConfiguration Configuration { get; set; }
        }
    }
}
