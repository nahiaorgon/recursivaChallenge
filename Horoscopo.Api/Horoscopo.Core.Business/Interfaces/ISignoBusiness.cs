using Horoscopo.Core.Entities;

namespace Horoscopo.Core.Business.Interfaces
{
    public interface ISignoBusiness
    {
        Task<bool> HistorialGuardarAsync(Historial historial);
        Task<List<Historial>> HistorialObtenerAsync();
    }
}
