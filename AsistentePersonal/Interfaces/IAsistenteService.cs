using AsistentePersonal.Models;

namespace AsistentePersonal.Interfaces
{
    public interface IAsistenteService
    {
        void AgregarUsuario(string nombre, string correo);

        string InterpretarComando(string comando, Usuario usuario);

        void AgregarTarea(string descripcion, int usuarioId);

        string EliminarTarea(int tareaId);

        Task<List<Historial>> ObtenerHistorialAsync(Usuario usuario);
    }
}
