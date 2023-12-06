using LoginBackend2023.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginBackend2023.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext dbContext;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
        }




        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = new IdentityUser
            {
                UserName = credencialesUsuario.Email,
                Email = credencialesUsuario.Email,
            };
            var resultado = await userManager.CreateAsync(usuario, credencialesUsuario.Password);
            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            return BadRequest(resultado.Errors);
        }

        private async Task<ActionResult<RespuestaAutenticacion>> ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
        {
            new Claim("email",credencialesUsuario.Email)
        };
            var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            var claimsRoles = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claims);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["LlaveJWT"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion
            {
                token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                expiracion = expiracion,
            };
        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<RespuestaAutenticacion>> Renovar()
        {

            var emailClaims = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Email).Select(x => x.Value).FirstOrDefault();
            var credencialesUsuario = new CredencialesUsuario() { Email = emailClaims };

            return await ConstruirToken(credencialesUsuario);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {
            var resultado = await signInManager.PasswordSignInAsync(
                credencialesUsuario.Email,
                credencialesUsuario.Password,
                isPersistent: false,
                lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }
        }



        [HttpPost("JuegoFavorito")]
        public async Task<ActionResult> JuegosFavoritos(JuegoFavorito juego)
            {
            var usuarioId = userManager.GetUserId(User);

            var juegoFavorito = new JuegoFavorito
            {
                UsuarioId = usuarioId,
                JuegoId = juego.JuegoId,
                // Puedes agregar más propiedades según sea necesario
            };

            dbContext.JuegosFavoritos.Add(juegoFavorito);
            await dbContext.SaveChangesAsync();

            return Ok();

        }


        [HttpGet("ObtenerJuegosFavoritos")]
        public ActionResult<IEnumerable<JuegoFavorito>> ObtenerJuegosFavoritos()
        {
            var usuarioId = userManager.GetUserId(User);
            var juegosFavoritos = dbContext.JuegosFavoritos
          .Where(j => j.UsuarioId == usuarioId)
          .ToList();

            // Lógica para obtener los juegos favoritos del usuario desde la base de datos
            // ...

            return Ok(juegosFavoritos);
        }



    }
}


