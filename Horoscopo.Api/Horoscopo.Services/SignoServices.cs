using Horoscopo.Core.Business.Interfaces;
using Horoscopo.Core.Entities;
using System.Net.Http.Json; 

namespace Horoscopo.Services
{
    public class SignoServices
    {
        private readonly HttpClient _httpClient; 
        private readonly ISignoBusiness _signoBusiness;
         
        public SignoServices(HttpClient httpClient,
                            ISignoBusiness signoBusiness)
        {
            _httpClient = httpClient;
            _signoBusiness = signoBusiness;

        }

        public async Task<string> ObtenerHoroscopoAsync(string signo)
        {
            var body = new
            {
                date = DateTime.UtcNow.ToString("yyyy-MM-dd"), 
                lang = "es", // Idioma español 
                sign = signo
            };

            var json = System.Text.Json.JsonSerializer.Serialize(body);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // Eliminamos el charset si es necesario
            content.Headers.ContentType.CharSet = "";

            var response = await _httpClient.PostAsync("https://newastro.vercel.app/", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Core.Entities.Horoscopo>();
                return result?.Prediccion ?? "No se encontró el horóscopo.";
            }
            else
            {
                var errorDetalle = await response.Content.ReadAsStringAsync(); 
                throw new Exception($"La API devolvió 400. Detalle: {errorDetalle}");
            }

          //      return "Error al conectar con la API de horóscopos.";
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
                1 => dia <= 20 ? "Capricornio" : "Acuario",
                2 => dia <= 19 ? "Acuario" : "Piscis",
                3 => dia <= 20 ? "Piscis" : "Aries",
                4 => dia <= 20 ? "Aries" : "Tauro",
                5 => dia <= 21 ? "Tauro" : "Géminis",
                6 => dia <= 21 ? "Géminis" : "Cáncer",
                7 => dia <= 23 ? "Cáncer" : "Leo",
                8 => dia <= 23 ? "Leo" : "Virgo",
                9 => dia <= 23 ? "Virgo" : "Libra",
                10 => dia <= 23 ? "Libra" : "Escorpio",
                11 => dia <= 22 ? "Escorpio" : "Sagitario",
                12 => dia <= 21 ? "Sagitario" : "Capricornio",
                _ => "Desconocido"
            };
        }
        public async Task<Core.Entities.Horoscopo> ProcesarConsultaCompletaAsync(Registro registro)
        {
            string signo = ObtenerSignoZodiacal(registro.FechaNacimiento);
            int diasParaCumple = CalcularDiasProximoCumple(registro.FechaNacimiento);  

            string relato = await ObtenerHoroscopoAsync(signo);

            var historial = new Historial
            {
                Nombre = registro.Nombre,
                Email = registro.Email,
                Signo = signo,
                FechaConsulta = DateTime.Now
            };

            bool guardado = await _signoBusiness.HistorialGuardarAsync(historial);

            return new Horoscopo.Core.Entities.Horoscopo
            {
                //Nombre = registro.Nombre,
               // Mensaje = $"Hola {registro.Nombre}",
                Signo = signo,
                Prediccion = relato,
                DiasParaCumple = diasParaCumple
            };
        }

        public async Task<List<Historial>> ObtenerHistorialAsync()
        {
            return await _signoBusiness.HistorialObtenerAsync();
        }

        public async Task<string> ObtenerSignoMasBuscadoAsync()
        {
            var historial = await _signoBusiness.HistorialObtenerAsync();

            if (!historial.Any()) return "No se encontró historial";

            var signoMasBuscado = historial
                .GroupBy(h => h.Signo)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .First();

            return signoMasBuscado;
        }
    }
}
