using AsistentePersonal.Models;

namespace AsistentePersonal.Interfaces
{
    public interface IUsuarioService
    {
        void AgregarUsuario(string nombre, string correo);
        void EliminarUsuario(int usuarioId);
        Task<Usuario?> ObtenerUsuarioPorId(int usuarioId);
    }
}
