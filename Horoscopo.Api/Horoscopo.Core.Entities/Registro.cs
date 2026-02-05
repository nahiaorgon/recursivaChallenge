using System.ComponentModel.DataAnnotations;

namespace Horoscopo.Core.Entities
{
    public class Registro
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime? FechaNacimiento { get; set; } = null;
    }
}
