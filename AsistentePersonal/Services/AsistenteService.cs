using AsistentePersonal.Data;
using AsistentePersonal.Interfaces;
using AsistentePersonal.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AsistentePersonal.Services
{
    public class AsistenteService : IAsistenteService
    {
        private readonly AsistenteDbContext _context;

        public AsistenteService(AsistenteDbContext context)
        {
            _context = context;
        }
        public void AgregarUsuario(string nombre, string correo)
        {
            var usuario = new Usuario
            {
                Nombre = nombre,
                CorreoElectronico = correo,
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public string InterpretarComando(string comando, Usuario usuario)
        {
            if (comando == "Tareas")
            {
                var tareas = _context.Tareas.ToList(); // Obtener tareas de la base de datos
                return string.Join("\n", tareas.Select(t => t.Descripcion));
            }

            return "Comando no reconocido";
        }

        public void AgregarTarea(string descripcion, int usuarioId)
        {
            var tarea = new Tarea
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

        public string EliminarTarea(int tareaId)
        {
            var tarea = _context.Tareas.Find(tareaId);
            if (tarea == null) return "Tarea no encontrada.";

            _context.Tareas.Remove(tarea);
            _context.SaveChanges();
            return $"Tarea '{tarea.Descripcion}' eliminada.";
        }

        public async Task<List<Historial>> ObtenerHistorialAsync(Usuario usuario)
        {
            return await _context.Historial
                 .Where(h => h.Usuario == usuario)
                 .OrderByDescending(h => h.Fecha)
                 .ToListAsync();
        }

        internal async Task<Usuario> ObtenerUsuarioPorId(int usuarioId)
        {
          return await _context.Usuarios.FindAsync(usuarioId);
        }
    }
}
