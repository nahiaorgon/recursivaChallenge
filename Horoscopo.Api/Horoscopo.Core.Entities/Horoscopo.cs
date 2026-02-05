using System.Text.Json.Serialization;

namespace Horoscopo.Core.Entities
{
    public class Horoscopo
    {  
            [JsonPropertyName("sign")]
            public string Signo { get; set; } = string.Empty;
             
            [JsonPropertyName("horoscope")]
            public string Prediccion { get; set; } = string.Empty;
             
            [JsonPropertyName("icon")]
            public string IconoUrl { get; set; } = string.Empty;

            [JsonPropertyName("date")]
            public string Fecha { get; set; } = string.Empty;

            [JsonPropertyName("id")]
            public int Id { get; set; }
             
            public int DiasParaCumple { get; set; }
        }
    }
 
