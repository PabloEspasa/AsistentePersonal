namespace AsistentePersonal.Models
{
    public class Historial
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
        public string Comando { get; set; }
        public string Respuesta { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
