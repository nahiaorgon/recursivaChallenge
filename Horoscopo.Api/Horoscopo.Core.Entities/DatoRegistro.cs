namespace Horoscopo.Core.Entities
{
    public class DatoRegistro
    { 
        public Consulta Datos { get; set; } = new Consulta { FechaNacimiento = DateTime.Today };

        public string GeneroSeleccionado { get; set; } = string.Empty;
    }
}
