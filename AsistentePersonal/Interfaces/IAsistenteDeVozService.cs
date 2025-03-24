namespace AsistentePersonal.Interfaces
{
    public interface IAsistenteDeVozService
    {
        void IniciarReconocimiento();
        void DetenerReconocimiento();
        void ResponderConVoz(string texto);
    }
}
