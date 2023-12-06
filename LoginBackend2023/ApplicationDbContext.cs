using LoginBackend2023.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginBackend2023;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions option) : base(option)
    {
    }
    public DbSet<JuegoFavorito> JuegosFavoritos { get; set; }

}
