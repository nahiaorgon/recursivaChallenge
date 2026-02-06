using System.ComponentModel.DataAnnotations;

namespace Horoscopo.Core.Entities
{
    public class Historial
    {
        [Key]
        public int HistorialId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Signo { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public DateTime FechaConsulta { get; set; } = DateTime.Now;
    }
}
