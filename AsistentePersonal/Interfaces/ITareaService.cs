using AsistentePersonal.Models;

namespace AsistentePersonal.Interfaces
{
    public interface ITareaService
    {
        void AgregarTarea(string descripcion, int usuarioId);
        string EliminarTarea(Guid tareaId, int usuarioId);
        List<Tarea> ObtenerTareas(int usuarioId);
    }
}
