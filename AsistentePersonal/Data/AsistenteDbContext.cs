using AsistentePersonal.Models;
using Microsoft.EntityFrameworkCore;

namespace AsistentePersonal.Data
{
    public class AsistenteDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Historial> Historial { get; set; }

        public AsistenteDbContext(DbContextOptions<AsistenteDbContext> options)
            : base(options)
        {
        }
    }
}
