using AsistentePersonal.Interfaces;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace AsistentePersonal.Services
{
    public class AsistenteDeVozService : IAsistenteDeVozService
    {
        private readonly SpeechRecognitionEngine _reconocedor;
        private readonly SpeechSynthesizer _sintetizador;
        private readonly ITareaService _tareaService;

        public AsistenteDeVozService(ITareaService tareaService)
        {
            _reconocedor = new SpeechRecognitionEngine();
            _sintetizador = new SpeechSynthesizer();
            _tareaService = tareaService;

            _reconocedor.SetInputToDefaultAudioDevice();

            _reconocedor.LoadGrammar(new DictationGrammar());

            _reconocedor.SpeechRecognized += Reconocedor_SpeechRecognized;
        }
        public void IniciarReconocimiento()
        {
            _reconocedor.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void Reconocedor_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string textoReconocido = e.Result.Text.ToLower();
            Console.WriteLine("Texto reconocido: " + textoReconocido);
            ProcesarComando(textoReconocido);
        }

        private void ProcesarComando(string comando)
        {
            if (comando.Contains("hola"))
            {
                ResponderConVoz("¡Hola! ¿En qué puedo ayudarte?");
            }
            else if (comando.Contains("adiós"))
            {
                ResponderConVoz("Adiós, ¡hasta pronto!");
                DetenerReconocimiento();
            }
            else if (comando.Contains("mostrar tareas"))
            {
                var tareas = _tareaService.ObtenerTareas(1);
                ResponderConVoz($"Tus tareas son: {string.Join(", ", tareas)}");
            }
            else
            {
                ResponderConVoz("Lo siento, no entiendo ese comando.");
            }
        }
      
        public void ResponderConVoz(string texto)
        {
            _sintetizador.Speak(texto);
        }
      
        public void DetenerReconocimiento()
        {
            _reconocedor.RecognizeAsyncStop();
        }
    }
}
