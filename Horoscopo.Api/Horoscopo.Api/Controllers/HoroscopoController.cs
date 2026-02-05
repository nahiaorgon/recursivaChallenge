using Horoscopo.Core.Entities;
using Horoscopo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Horoscopo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoroscopoController : ControllerBase
    {
        private readonly SignoServices _signoService;
        public HoroscopoController(SignoServices signoService)
        {
            _signoService = signoService;
        }

        [HttpPost("consultar")]
        public async Task<IActionResult> ConsultarHoroscopo([FromBody] Consulta consulta)
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

        [HttpGet("signo-mas-buscado")]
        public async Task<IActionResult> GetSignoMasBuscado()
        {
            var signo = await _signoService.ObtenerSignoMasBuscadoAsync();
            return Ok(new { signoMasBuscado = signo });
        }
    }
     
}