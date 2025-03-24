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
            _asistenteDeVozService.IniciarReconocimiento();
            return Ok("Reconocimiento de voz iniciado. Di algo.");
        }
      
        [HttpPost("detener-reconocimiento")]
        public IActionResult DetenerReconocimiento()
        {
            _asistenteDeVozService.DetenerReconocimiento();
            return Ok("Reconocimiento de voz detenido.");
        }
       
        [HttpPost("responder")]
        public IActionResult Responder([FromBody] string texto)
        {
            _asistenteDeVozService.ResponderConVoz(texto);
            return Ok("Respuesta hablada: " + texto);
        }
    }
}
