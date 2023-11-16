namespace LoginBackend2023.Models
{
    public class RespuestaAutenticacion
    {
        //se requiere clve jwt, ir a appsettigs para crearla

        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
