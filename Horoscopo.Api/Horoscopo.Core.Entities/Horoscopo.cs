using System.Text.Json.Serialization;

namespace Horoscopo.Core.Entities
{
    public class Horoscopo
    {  
            [JsonPropertyName("sign")]
            public string Signo { get; set; } = string.Empty;
             
            [JsonPropertyName("horoscope")]
            public string Prediccion { get; set; } = string.Empty;
            public int DiasParaCumple { get; set; }
        }
    }
 
