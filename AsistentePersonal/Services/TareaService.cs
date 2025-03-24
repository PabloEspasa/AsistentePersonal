using AsistentePersonal.Data;
using AsistentePersonal.Interfaces;
using AsistentePersonal.Models;
using Microsoft.EntityFrameworkCore;

namespace AsistentePersonal.Services
{
    public class TareaService : ITareaService
    {
        private readonly AsistenteDbContext _context;


        public TareaService(AsistenteDbContext context)
        {
            _context = context;
        }

        public void AgregarTarea(string descripcion, int usuarioId)
        {
            Tarea tarea = new Tarea
            {
                Descripcion = descripcion,
                FechaCreacion = DateTime.Now,
                Estado = EstadoTarea.Pendiente,
                Prioridad = PrioridadTarea.Alta,
                Completado = false,
                UsuarioId = usuarioId
            };

            _context.Tareas.Add(tarea);
            _context.SaveChanges();
        }

        public string EliminarTarea(Guid tareaId, int usuarioId)
        {
            Tarea? tarea = _context.Tareas.FirstOrDefault(t => t.Id == tareaId && t.UsuarioId == usuarioId);

            if (tarea == null)
                return "Tarea no encontrada o no pertenece al usuario.";

            _context.Tareas.Remove(tarea);
            _context.SaveChanges();

            return $"Tarea '{tarea.Descripcion}' eliminada.";
        }
        
        public List<Tarea> ObtenerTareas(int usuarioId)
        {
            Usuario? usuario = _context.Usuarios.Include(u => u.Tareas).FirstOrDefault(u => u.Id == usuarioId);

            ICollection<Tarea> tareas = usuario.Tareas;

            if (tareas.Count == 0)
                return new List<Tarea>();

            return tareas.ToList();
        }
    }
}
