using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//CONFIGURAREMOS ORM
namespace LoginBackend2023
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)//hereda option de identity a application
        {

        }
    }
}
