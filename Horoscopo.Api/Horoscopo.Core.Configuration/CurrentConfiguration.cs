using Horoscopo.Core.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Horoscopo.Core.Configuration
{
    public class CurrentConfiguration : ICurrentConfiguration
    {
        public IHoroscopoConfig Horoscopo { get; set; }

        public CurrentConfiguration() { }

        public static ICurrentConfiguration Build(IConfiguration configuration, IHostEnvironment hostEnviroment)
        {
            return new CurrentConfiguration()
            {
                Horoscopo = HoroscopoConfig.Build(configuration)
            };
        }
    }
}
