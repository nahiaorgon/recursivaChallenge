using Horoscopo.Core.Configuration.Interfaces;
using Microsoft.Extensions.Configuration; 

namespace Horoscopo.Core.Configuration
{
    public class HoroscopoConfig :  IHoroscopoConfig
    {
        public string ApiUrl { get; set; }
        public HoroscopoConfig()
        {
        }

        public static HoroscopoConfig Build(IConfiguration configuration)
        {
            var config = new HoroscopoConfig();

            config.ApiUrl = configuration["Horoscopo:ApiUrl"] ?? throw new Exception("La configuración 'HoroscopoApiUrl' no fue encontrada.");

            return config;
        }
    }
}
