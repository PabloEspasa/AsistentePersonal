using AsistentePersonal.DTOs.Request;
using AsistentePersonal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AsistentePersonal.Controllers
{
    public class AsistenteDeVozController : ControllerBase
    {
        private readonly IAsistenteDeVozService _asistenteDeVozService;

        public AsistenteDeVozController(IAsistenteDeVozService asistenteDeVozService)
        {
            _asistenteDeVozService = asistenteDeVozService;
        }

        [HttpPost("iniciar-reconocimiento")]
        public IActionResult IniciarReconocimiento()
        {
            try
            {
                _asistenteDeVozService.IniciarReconocimiento();
                return Ok("Reconocimiento de voz iniciado. Di algo.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al iniciar el reconocimiento: {ex.Message}");
            }
        }

        [HttpPost("detener-reconocimiento")]
        public IActionResult DetenerReconocimiento()
        {
            _asistenteDeVozService.DetenerReconocimiento();
            return Ok("Reconocimiento de voz detenido.");
        }

        [HttpPost("responder")]
        public IActionResult Responder([FromBody] ResponderRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Texto))
            {
                return BadRequest("El texto no puede estar vacío.");
            }

            _asistenteDeVozService.ResponderConVoz(request.Texto);
            return Ok($"Respuesta hablada: {request.Texto}");
        }
    }
}
