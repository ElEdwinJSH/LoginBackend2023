using System.ComponentModel.DataAnnotations;

namespace LoginBackend2023.Models
{
    public class JuegoFavorito
    {

        [Key]
        public int Id { get; set; }

        public string UsuarioId { get; set; }

        public string JuegoId { get; set; }

        // Otros campos relacionados con el juego favorito
    }
}
