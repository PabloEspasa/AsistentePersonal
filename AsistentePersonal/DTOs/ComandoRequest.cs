using AsistentePersonal.Models;

namespace AsistentePersonal.DTOs
{
    public class ComandoRequest
    {
        public string Comando { get; set; }
        public Usuario Usuario { get; set; }
    }
}
