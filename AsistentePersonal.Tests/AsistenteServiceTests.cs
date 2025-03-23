using AsistentePersonal.Services;

namespace AsistentePersonal.Tests
{
    public class AsistenteServiceTests
    {
        private readonly AsistenteService _asistenteService;

        public AsistenteServiceTests()
        {
            _asistenteService = new AsistenteService();
        }

        [Fact]
        public void ProcesarComando_DeberiaRetornarRespuestaEsperada()
        {

            string comando = "Hola";
            string expectedResponse = "No entiendo el comando";

            string resultado = _asistenteService.InterpretarComando(comando);

            Assert.Equal(expectedResponse, resultado);
        }

        [Fact]
        public void ProcesarComandoNull_DeberiaRetornarRespuestaEsperada()
        {

            string? comando = null;
            string expectedResponse = "El comando no puede estar vació";

            string resultado = _asistenteService.InterpretarComando(comando);

            Assert.Equal(expectedResponse, resultado);
        }

        [Fact]
        public void ProcesarComandoHora_DeberiaRetornarRespuestaEsperada()
        {

            string comando = "Hora";
            string expectedResponse = $"Son las {DateTime.Now:HH:mm}";

            string resultado = _asistenteService.InterpretarComando(comando);

            Assert.Equal(expectedResponse, resultado);
        }

        [Fact]
        public void ProcesarComandoFecha_DeberiaRetornarRespuestaEsperada()
        {

            string comando = "Fecha";
            string expectedResponse = $"Hoy es {DateTime.Today}";

            string resultado = _asistenteService.InterpretarComando(comando);

            Assert.Equal(expectedResponse, resultado);
        }
    }
}
