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

            var response = await _httpClient.PostAsJsonAsync("https://newastro.vercel.app", body);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Core.Entities.Horoscopo>();
                return result?.Descripcion ?? "No se encontró el horóscopo.";
            }

            return "Error al conectar con la API de horóscopos.";
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
        public async Task<object> ProcesarConsultaCompletaAsync(Consulta consulta)
        {
            string signo = ObtenerSignoZodiacal(consulta.FechaNacimiento);
            int diasParaCumple = CalcularDiasProximoCumple(consulta.FechaNacimiento); // REQUISITO 

            string relato = await ObtenerHoroscopoAsync(signo);

            var historial = new Historial
            {
                Nombre = consulta.Nombre,
                Email = consulta.Email,
                Signo = signo,
                FechaConsulta = DateTime.Now
            };

            bool guardado = await _signoBusiness.HistorialGuardarAsync(historial);

            return new
            {
                Nombre = consulta.Nombre,
                Mensaje = $"Hola {consulta.Nombre}",
                Signo = signo,
                Horoscopo = relato,
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
