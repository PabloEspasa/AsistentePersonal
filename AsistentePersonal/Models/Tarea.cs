namespace AsistentePersonal.Models
{
    public enum EstadoTarea
    {
        Pendiente,
        Completada,
        Cancelada
    }

    public enum PrioridadTarea
    {
        Alta,
        Media,
        Baja
    }
    public class Tarea
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public EstadoTarea Estado { get; set; }
        public PrioridadTarea Prioridad { get; set; }
        public int UsuarioId { get; set; }
        public bool Completado { get; set; }
        public string? Comentarios { get; set; }
        public DateTime? FechaCompletado { get; set; }


        public virtual Usuario Usuario { get; set; }

    }
}
