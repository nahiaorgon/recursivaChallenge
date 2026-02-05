
using Horoscopo.Core.Entities;

namespace Horoscopo.Services.Interfaces
{
    public interface ISignoServices
    {
        Task<Core.Entities.Horoscopo> ProcesarConsultaCompletaAsync(Registro registro);
        Task<List<Historial>> ObtenerHistorialAsync(); 
        Task<Estadistica> ObtenerEstadisticasSignoAsync();
    }
}
