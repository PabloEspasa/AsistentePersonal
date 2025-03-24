namespace AsistentePersonal.DTOs.Response
{
    public class AgregarUsuarioResponse : BaseResponse
    {
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }
}
