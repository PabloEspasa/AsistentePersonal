using AsistentePersonal.DTOs;
using AsistentePersonal.DTOs.Request;
using AsistentePersonal.DTOs.Response;
using AsistentePersonal.Interfaces;
using AsistentePersonal.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsistentePersonal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenteController : ControllerBase
    {
        private readonly IAsistenteService _asistenteService;
        private readonly IUsuarioService _usuarioService;

        // Constructor
        public AsistenteController(IAsistenteService asistenteService, IUsuarioService usuarioService)
        {
            _asistenteService = asistenteService;
            _usuarioService = usuarioService;
        }

        [HttpPost("InterpretarComando")]
        public IActionResult InterpretarComando([FromBody] ComandoRequest comandoRequest)
        {
            if (comandoRequest == null || string.IsNullOrEmpty(comandoRequest.Comando))
                return BadRequest("El comando es necesario.");

            string resultado = _asistenteService.InterpretarComando(comandoRequest.Comando, comandoRequest.UsuarioId);

            InterpretarComandoResponse response = new InterpretarComandoResponse
            {
                Resultado = resultado,
                Mensaje = string.IsNullOrEmpty(resultado) ? "Comando no reconocido" : "Comando procesado correctamente",
                Exitoso = !string.IsNullOrEmpty(resultado)
            };

            return Ok(response);
        }

        [HttpGet("Historial/{usuarioId}")]
        public async Task<IActionResult> ObtenerHistorial(int usuarioId)
        {
            Usuario? usuario = await _usuarioService.ObtenerUsuarioPorId(usuarioId);
            if (usuario == null)
                return NotFound(new ObtenerHistorialResponse
                {
                    Mensaje = "Usuario no encontrado.",
                    Exitoso = false,
                    Historial = new List<HistorialDto>()
                });

            List<Historial> historial = await _asistenteService.ObtenerHistorialAsync(usuario);

            List<HistorialDto> historialDto = historial.Select(h => new HistorialDto
            {
                Fecha = h.Fecha,
                Comando = h.Comando,
                Respuesta = h.Respuesta
            }).ToList();

            var response = new ObtenerHistorialResponse
            {
                Historial = historialDto,
                Mensaje = historialDto.Count > 0 ? "Historial obtenido correctamente." : "No se encontraron registros.",
                Exitoso = historialDto.Count > 0
            };
            return Ok(response);
        }
    }
}
