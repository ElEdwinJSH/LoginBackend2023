using System.ComponentModel.DataAnnotations;

namespace LoginBackend2023.Models
{
    public class CredencialesUsuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
