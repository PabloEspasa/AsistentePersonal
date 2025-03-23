using AsistentePersonal.Interfaces;

namespace AsistentePersonal.Services
{
    public class AsistenteService : IAsistenteService
    {
        public string InterpretarComando(string comando)
        {
            if (string.IsNullOrEmpty(comando))
            {
                return "El comando no puede estar vació";
            }
            if (comando.Contains("hora", StringComparison.OrdinalIgnoreCase))
            {
                return $"Son las {DateTime.Now:HH:mm}";
            }
            if (comando.Contains("fecha", StringComparison.OrdinalIgnoreCase))
            {
                return $"Hoy es {DateTime.Today}";
            }
            return "No entiendo el comando";
        }
    }
}
