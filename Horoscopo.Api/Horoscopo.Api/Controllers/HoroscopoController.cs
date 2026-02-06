using Horoscopo.Core.Entities;
using Horoscopo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Horoscopo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoroscopoController : ControllerBase
    {
        private readonly ISignoServices _signoService;
        public HoroscopoController(ISignoServices signoService)
        {
            _signoService = signoService;
        }

        [HttpPost("consultar")]
        public async Task<IActionResult> ConsultarHoroscopo([FromBody] Registro consulta)
        {
            if (consulta == null) return BadRequest();

            var resultado = await _signoService.ProcesarConsultaCompletaAsync(consulta);

            if (resultado == null) return StatusCode(500, "Error al procesar la consulta");

            return Ok(resultado);
        }

        [HttpGet("historial")]
        public async Task<IActionResult> GetHistorial()
        {
            var historial = await _signoService.ObtenerHistorialAsync();
            return Ok(historial);
        }

        [HttpGet("estadisticas")]
        public async Task<IActionResult> GetSignoMasBuscado()
        {
            var signo = await _signoService.ObtenerEstadisticasSignoAsync();
            return Ok(signo);
        }
    }
     
}