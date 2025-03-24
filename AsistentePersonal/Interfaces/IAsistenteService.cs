using AsistentePersonal.Models;

namespace AsistentePersonal.Interfaces
{
    public interface IAsistenteService
    {
        string InterpretarComando(string comando, int usuarioId);
        Task<List<Historial>> ObtenerHistorialAsync(Usuario usuario);
    }
}
