using AsistentePersonal.DTOs.Request;
using AsistentePersonal.DTOs.Response;
using AsistentePersonal.Interfaces;
using AsistentePersonal.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsistentePersonal.Controllers
{
    public class TareaController : Controller
    {
        private readonly ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpPost("AgregarTarea")]
        public IActionResult AgregarTarea([FromBody] AgregarTareaRequest tareaRequest)
        {
            if (tareaRequest == null || tareaRequest.Descripcion == null)
                return BadRequest("Los datos de la tarea son necesarios.");
            try
            {
                _tareaService.AgregarTarea(tareaRequest.Descripcion, tareaRequest.UsuarioId);
            }
            catch (Exception ex)
            {
                return BadRequest(new AgregarTareaResponse
                {
                    Exitoso = false,
                    Mensaje = $"Ocurrió un error al agregar el usuario: {ex.Message}"
                });
            }

            AgregarTareaResponse response = new AgregarTareaResponse
            {
                Descripcion = tareaRequest.Descripcion,
                FechaVencimiento = tareaRequest.FechaVencimiento ?? DateTime.MinValue,
                UsuarioId = tareaRequest.UsuarioId,
                Comentarios = tareaRequest.Comentarios,
                Exitoso = true,
                Mensaje = "Tarea agregada correctamente."
            };
            return Ok(response);
        }

        [HttpGet("ObtenerTareas/{usuarioId}")]
        public IActionResult ObtenerTareas(int usuarioId)
        {
            var resultado = _tareaService.ObtenerTareas(usuarioId);

            if (resultado.Count == 0)
                return NotFound("No se encontraron tareas para el usuario.");

            return Ok(resultado);
        }

        [HttpDelete("EliminarTarea/{usuarioId}/{tareaId}")]
        public IActionResult EliminarTarea(Guid tareaId, int usuarioId)
        {
            string resultado = _tareaService.EliminarTarea(tareaId, usuarioId);

            if (resultado.StartsWith("Tarea"))
                return Ok(resultado);

            return NotFound(resultado);
        }
    }
}
