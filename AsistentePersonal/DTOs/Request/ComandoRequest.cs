using AsistentePersonal.Models;

namespace AsistentePersonal.DTOs.Request
{
    public class ComandoRequest
    {
        public string Comando { get; set; }
        public int UsuarioId { get; set; }
    }
}
