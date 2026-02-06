using Horoscopo.Core.Business.Interfaces;
using Horoscopo.Core.Configuration.Interfaces;
using Horoscopo.Core.Entities;
using Horoscopo.Services.Interfaces;
using System.Net.Http.Json; 

namespace Horoscopo.Services
{
    public class SignoServices : ISignoServices
    {
        private readonly HttpClient _httpClient; 
        private readonly ISignoBusiness _signoBusiness;
        private readonly string _baseUrl;
        public SignoServices(HttpClient httpClient,
                            ISignoBusiness signoBusiness,
                            IHoroscopoConfig config)
        {
            _httpClient = httpClient;
            _signoBusiness = signoBusiness;
            _baseUrl = config.ApiUrl;
        }

        public async Task<string> ObtenerHoroscopoAsync(string signo)
        {
            var body = new
            {
                date = DateTime.UtcNow.ToString("yyyy-MM-dd"), 
                lang = "es", 
                sign = signo
            };

            var json = System.Text.Json.JsonSerializer.Serialize(body);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Core.Entities.Horoscopo>();
                return result?.Prediccion ?? "No se encontró el horóscopo.";
            }

            return "Error al obtener los datos del horóscopo.";
        }

        public int CalcularDiasProximoCumple(DateTime fechaNacimiento)
        {
            DateTime hoy = DateTime.Today;
            DateTime proximoCumple = new DateTime(hoy.Year, fechaNacimiento.Month, fechaNacimiento.Day);

            if (proximoCumple < hoy)
            {
                proximoCumple = proximoCumple.AddYears(1);
            }

            return (proximoCumple - hoy).Days;
        }

        public string ObtenerSignoZodiacal(DateTime fecha)
        {
            int dia = fecha.Day;
            int mes = fecha.Month;

            return mes switch
            {
                1 => dia <= 20 ? "Capricorn" : "Aquarius",
                2 => dia <= 19 ? "Aquarius" : "Pisces",
                3 => dia <= 20 ? "Pisces" : "Aries",
                4 => dia <= 20 ? "Aries" : "Taurus",
                5 => dia <= 21 ? "Taurus" : "Gemini",
                6 => dia <= 21 ? "Gemini" : "Cancer",
                7 => dia <= 23 ? "Cancer" : "Leo",
                8 => dia <= 23 ? "Leo" : "Virgo",
                9 => dia <= 23 ? "Virgo" : "Libra",
                10 => dia <= 23 ? "Libra" : "Scorpio",
                11 => dia <= 22 ? "Scorpio" : "Sagittarius",
                12 => dia <= 21 ? "Sagittarius" : "Capricorn",
                _ => "Desconocido"
            };
        }
        public async Task<Core.Entities.Horoscopo> ProcesarConsultaCompletaAsync(Registro registro)
        {
            string signo = ObtenerSignoZodiacal(registro.FechaNacimiento.Value);
            int diasParaCumple = CalcularDiasProximoCumple(registro.FechaNacimiento.Value);

            if (!registro.RegistroConsultado)
            { 
                var historial = new Historial
                {
                    Nombre = registro.Nombre,
                    Email = registro.Email,
                    Signo = signo,
                    Genero = registro.Genero,
                    FechaConsulta = DateTime.Now
                };

                bool guardado = await _signoBusiness.HistorialGuardarAsync(historial);

                if (guardado)
                {
                    //Aca pondria un log para saber si se esta guardando o no mis consultas.
                }
            }

            string relato = await ObtenerHoroscopoAsync(signo);

            return new Core.Entities.Horoscopo
            { 
                Signo = signo,
                Prediccion = relato,
                DiasParaCumple = diasParaCumple
            };
        }

        public async Task<List<Historial>> ObtenerHistorialAsync()
        {
            return await _signoBusiness.HistorialObtenerAsync();
        }
         
        public async Task<Estadistica> ObtenerEstadisticasSignoAsync()
        {
            var historial = await _signoBusiness.HistorialObtenerAsync();

            var signoMasBuscado = historial
                .GroupBy(h => h.Signo)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Signo = g.Key, Cantidad = g.Count() })
                .FirstOrDefault();

            return new Estadistica
            {
                Historial = historial.OrderByDescending(h => h.FechaConsulta).ToList(),
                SignoFavorito = signoMasBuscado?.Signo ?? "N/A",
                TotalConsultas = historial.Count
            };
        }
    }
}
