using AsistentePersonal.DTOs.Request;
using AsistentePersonal.DTOs.Response;
using AsistentePersonal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AsistentePersonal.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("AgregarUsuario")]
        public IActionResult AgregarUsuario([FromBody] AgregarUsuarioRequest usuarioRequest)
        {
            if (usuarioRequest == null)
                return BadRequest("Los datos del usuario son necesarios.");

            try
            {
                _usuarioService.AgregarUsuario(usuarioRequest.Nombre, usuarioRequest.CorreoElectronico);
            }
            catch (Exception ex)
            {
                return BadRequest(new AgregarUsuarioResponse
                {
                    Exitoso = false,
                    Mensaje = $"Ocurrió un error al agregar el usuario: {ex.Message}"
                });
            }

            AgregarUsuarioResponse response = new AgregarUsuarioResponse
            {
                Nombre = usuarioRequest.Nombre,
                CorreoElectronico = usuarioRequest.CorreoElectronico,
                FechaRegistro = DateTime.Now,
                Activo = true,
                Exitoso = true,
                Mensaje = "Usuario agregado correctamente."
            };

            return Ok(response);
        }

        [HttpDelete("EliminarUsuario/{usuarioId}")]
        public IActionResult EliminarUsuario(int usuarioId)
        {
            try
            {
                _usuarioService.EliminarUsuario(usuarioId);
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    Exitoso = false,
                    Mensaje = $"Ocurrió un error al eliminar el usuario: {ex.Message}"
                });
            }
            return Ok(new BaseResponse
            {
                Exitoso = true,
                Mensaje = "Usuario eliminado correctamente."
            });
        }
    }
}
