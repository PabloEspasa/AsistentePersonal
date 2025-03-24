namespace AsistentePersonal.DTOs.Request
{
    public class AgregarTareaRequest
    {
        public string Descripcion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int UsuarioId { get; set; }
        public string Comentarios { get; set; }
    }
}
