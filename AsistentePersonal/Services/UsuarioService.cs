using AsistentePersonal.Data;
using AsistentePersonal.Interfaces;
using AsistentePersonal.Models;
using Microsoft.EntityFrameworkCore;

namespace AsistentePersonal.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AsistenteDbContext _context;

        public UsuarioService(AsistenteDbContext context)
        {
            _context = context;
        }

        public void AgregarUsuario(string nombre, string correo)
        {
            Usuario usuario = new Usuario
            {
                Nombre = nombre,
                CorreoElectronico = correo,
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void EliminarUsuario(int usuarioId)
        {
            Usuario ?usuario = _context.Usuarios.Include(u => u.Tareas).FirstOrDefault(u => u.Id == usuarioId);

            if (usuario.Tareas.Count != 0)
            {
                _context.Tareas.RemoveRange(usuario.Tareas);
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

        public async Task<Usuario?> ObtenerUsuarioPorId(int usuarioId)
        {
            return await _context.Usuarios.FindAsync(usuarioId);
        }

    }
}
