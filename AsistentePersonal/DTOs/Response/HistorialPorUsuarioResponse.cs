namespace AsistentePersonal.DTOs.Response
{
    public class HistorialPorUsuarioResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Comando { get; set; }
        public string Respuesta { get; set; }
        public DateTime Fecha { get; set; }
    }
}
