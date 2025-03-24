namespace AsistentePersonal.DTOs.Response
{
    public class ObtenerHistorialResponse
    {
        public required List<HistorialDto> Historial { get; set; }
        public required string Mensaje { get; set; }
        public bool Exitoso { get; set; }
    }
}