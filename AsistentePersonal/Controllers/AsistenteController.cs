using AsistentePersonal.Interfaces;
using AsistentePersonal.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsistentePersonal.Controllers
{
    [ApiController]
    [Route("api/asistente")]
    public class AsistenteController : ControllerBase
    {
        private readonly IAsistenteService _asistenteService;

        public AsistenteController(IAsistenteService asistenteService)
        {
            _asistenteService = asistenteService;
        }

        [HttpPost]
        public IActionResult ProcesarComando([FromBody] ComandoRequest comandoRequest)
        {
            string respuesta = _asistenteService.InterpretarComando(comandoRequest.Comando);
            return Ok(new { respuesta });
        }
    }
}
