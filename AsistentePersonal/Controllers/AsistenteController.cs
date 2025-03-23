using AsistentePersonal.DTOs;
using AsistentePersonal.Models;
using AsistentePersonal.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsistentePersonal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenteController : ControllerBase
    {
        private readonly AsistenteService _asistenteService;

        // Constructor
        public AsistenteController(AsistenteService asistenteService)
        {
            _asistenteService = asistenteService;
        }

        [HttpPost("AgregarUsuario")]
        public IActionResult AgregarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Los datos del usuario son necesarios.");

            _asistenteService.AgregarUsuario(usuario.Nombre, usuario.CorreoElectronico);
            return Ok("Usuario agregado correctamente.");
        }

        [HttpPost("InterpretarComando")]
        public IActionResult InterpretarComando([FromBody] ComandoRequest comandoRequest)
        {
            if (comandoRequest == null || string.IsNullOrEmpty(comandoRequest.Comando) || comandoRequest.Usuario == null)
                return BadRequest("El comando y el usuario son necesarios.");

            var respuesta = _asistenteService.InterpretarComando(comandoRequest.Comando, comandoRequest.Usuario);
            return Ok(respuesta);
        }

        [HttpPost("AgregarTarea")]
        public IActionResult AgregarTarea([FromBody] Tarea tarea)
        {
            if (tarea == null || tarea.Descripcion == null)
                return BadRequest("Los datos de la tarea son necesarios.");

            _asistenteService.AgregarTarea(tarea.Descripcion, tarea.UsuarioId);
            return Ok("Tarea agregada correctamente.");
        }

        [HttpDelete("EliminarTarea/{tareaId}")]
        public IActionResult EliminarTarea(int tareaId)
        {
            var resultado = _asistenteService.EliminarTarea(tareaId);
            if (resultado.StartsWith("Tarea"))
                return Ok(resultado);

            return NotFound(resultado);
        }

        [HttpGet("Historial/{usuarioId}")]
        public async Task<IActionResult> ObtenerHistorial(int usuarioId)
        {
            Usuario usuario = await _asistenteService.ObtenerUsuarioPorId(usuarioId);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            List<Historial> historial = await _asistenteService.ObtenerHistorialAsync(usuario);
            return Ok(historial);
        }
    }
}
