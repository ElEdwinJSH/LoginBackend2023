using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginBackend2023.Models
{
    public class JuegoFavorito
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string JuegoId { get; set; }

        // Otros campos relacionados con el juego favorito
    }
}
