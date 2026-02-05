

using Horoscopo.Core.Entities;

namespace Horoscopo.Core.Repository.Interfaces
{
    public interface ISignoRepository
    {
        Task<bool> HistorialGuardarAsync(Historial historial);
        Task<List<Historial>> HistorialObtenerAsync();
    }
}
