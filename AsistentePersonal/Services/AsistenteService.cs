using AsistentePersonal.Data;
using AsistentePersonal.Interfaces;
using AsistentePersonal.Models;
using Microsoft.EntityFrameworkCore;

namespace AsistentePersonal.Services
{
    public class AsistenteService : IAsistenteService
    {
        private readonly AsistenteDbContext _context;
        private readonly ITareaService tareaService;

        public AsistenteService(AsistenteDbContext context, ITareaService tareaService)
        {
            _context = context;
        }               

        public async Task<List<Historial>> ObtenerHistorialAsync(Usuario usuario)
        {
            return await _context.Historial
                 .Where(h => h.Usuario == usuario)
                 .OrderByDescending(h => h.Fecha)
                 .ToListAsync();
        }    

        public string InterpretarComando(string comando, int usuarioId)
        {
            if (string.IsNullOrWhiteSpace(comando))
                return "Comando no reconocido.";

            switch (comando.ToLower())
            {
                case "fecha":
                    return $"Fecha actual: {DateTime.Now.ToShortDateString()}";

                case "hora":
                    return $"Hora actual: {DateTime.Now.ToShortTimeString()}";

                case "tareas":
                    List<Tarea> tareas = tareaService.ObtenerTareas(usuarioId);
                    return tareas.Count != 0 ? string.Join("\n", tareas) : "No tienes tareas asignadas.";

                case "tareas completadas":
                    List<Tarea> completadas = tareaService.ObtenerTareas(usuarioId).Where(t=> t.Estado == EstadoTarea.Completada).ToList();
                    return completadas.Count != 0 ? string.Join("\n", completadas) : "No tienes tareas completadas.";

                case "tareas pendientes":
                    List<Tarea> pendientes = tareaService.ObtenerTareas(usuarioId).Where(t=> t.Estado == EstadoTarea.Pendiente).ToList();
                    return pendientes.Count != 0 ? string.Join("\n", pendientes) : "No tienes tareas pendientes.";

                default:
                    return "Comando no reconocido.";
            };
        }
    }
}
