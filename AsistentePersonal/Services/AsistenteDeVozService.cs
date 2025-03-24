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
            try
            {
                _reconocedor = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("es-ES"));
                _reconocedor.SetInputToDefaultAudioDevice();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al inicializar el reconocimiento de voz: " + ex.Message);
                ResponderConVoz("Hubo un problema con el reconocimiento de voz.");
            }
            _sintetizador = new SpeechSynthesizer();
            _tareaService = tareaService;

            Choices comandos = new Choices(new string[] { "hola", "adiós", "chau", "abrir navegador", "abrir crom", "mostrar tareas" });
         
            _reconocedor.SpeechRecognized += Reconocedor_SpeechRecognized;
        }

        private bool _reconociendo = false;

        public void IniciarReconocimiento()
        {
            if (!_reconociendo)
            {
                if (_reconocedor.Grammars.Count == 0)
                {
                    _reconocedor.LoadGrammar(new DictationGrammar());
                }

                _reconociendo = true;
                _reconocedor.RecognizeAsync(RecognizeMode.Multiple);
            }
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
            else if (comando.Contains("adiós") || comando.Contains("chau"))
            {
                ResponderConVoz("Adiós, ¡hasta pronto!");
                DetenerReconocimiento();
            }
            else if (comando.Contains("tarea") || comando.Contains("mostrar tarea"))
            {
                List<Models.Tarea> tareas = _tareaService.ObtenerTareas(1);
                if (tareas.Count != 0)
                {
                    ResponderConVoz($"Tus tareas son: {string.Join(", ", tareas)}");
                }
                else
                {
                    ResponderConVoz("No tienes tareas pendientes.");
                }
            }
            else if (comando.Contains("abrir navegador") || comando.Contains("abrir crom"))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "https://www.google.com",
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al abrir el navegador: " + ex.Message);
                    ResponderConVoz("No pude abrir el navegador.");
                }
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
            if (_reconociendo)
            {
                _reconociendo = false;
                _reconocedor.RecognizeAsyncStop();
            }
        }
    }
}
