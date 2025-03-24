using AsistentePersonal.Models;

namespace AsistentePersonal.DTOs.Response
{
    public class TareasPorUsuarioResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public EstadoTarea Estado { get; set; }
        public PrioridadTarea Prioridad { get; set; }
        public bool Completado { get; set; }
    }
}
