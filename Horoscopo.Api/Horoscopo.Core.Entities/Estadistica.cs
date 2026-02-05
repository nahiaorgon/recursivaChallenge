namespace Horoscopo.Core.Entities
{
    public class Estadistica
    {
        public List<Historial> Historial { get; set; } = new();
        public string SignoFavorito { get; set; } = "";
        public int TotalConsultas { get; set; }
    }
}
