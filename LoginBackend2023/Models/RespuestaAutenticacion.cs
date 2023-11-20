namespace LoginBackend2023.Models;

public class RespuestaAutenticacion
{
    public string token { get; set; }
    public DateTime expiracion { get; set; }
}
