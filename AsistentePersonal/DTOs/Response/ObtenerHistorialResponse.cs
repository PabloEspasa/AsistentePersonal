namespace AsistentePersonal.DTOs.Response
{
    public class ObtenerHistorialResponse
    {
        public List<HistorialDto> Historial { get; set; }
        public string Mensaje { get; set; }
        public bool Exitoso { get; set; }
    }
}