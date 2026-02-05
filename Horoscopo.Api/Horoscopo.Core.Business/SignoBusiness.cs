using Horoscopo.Core.Business.Interfaces;
using Horoscopo.Core.Entities;
using Horoscopo.Core.Repository.Interfaces;

namespace Horoscopo.Core.Business
{
    public class SignoBusiness : ISignoBusiness
    {
        public ISignoRepository _signoRepository;
        public SignoBusiness(ISignoRepository signoRepository)
        {
            _signoRepository = signoRepository;
        }

        public async Task<bool> HistorialGuardarAsync(Historial historial)
        {
            return await _signoRepository.HistorialGuardarAsync(historial);
        }

        public async Task<List<Historial>> HistorialObtenerAsync()
        {
            return await _signoRepository.HistorialObtenerAsync();
        }
    }

}
