using System.IdentityModel.Tokens.Jwt;             // Para crear y manejar tokens JWT
using System.Security.Claims;                      // Para agregar información (claims) al token
using System.Security.Cryptography;
using System.Text;
using ApiBienesRaices.Data;                        // Para acceder al contexto de base de datos
using ApiBienesRaices.Repository.IRepository;      // Interfaces de repositorios
using Microsoft.AspNetCore.Authorization;          // Para usar [Authorize]
using Microsoft.AspNetCore.Identity;               // Para el PasswordHasher
using Microsoft.AspNetCore.Mvc;                    // Controladores y rutas HTTP
using Microsoft.IdentityModel.Tokens;              // Para generar firmas y claves de token

namespace ApiBienesRaices.Controllers
{
    // Define la ruta base del controlador: /api/Propietarios
    [Route("api/[controller]")]
    [ApiController]
    public class PropietariosController : ControllerBase
    {
        // Dependencias inyectadas
        private readonly IRepositoryPropietarios repoPropietarios; // Acceso a los datos de propietarios
        private readonly IConfiguration _config;                   // Acceso a appsettings.json
        private readonly IWebHostEnvironment _environment;          // Para trabajar con archivos y rutas físicas

        // Constructor con inyección de dependencias
        public PropietariosController(IRepositoryPropietarios repo, IConfiguration config, IWebHostEnvironment env)
        {
            repoPropietarios = repo;
            _config = config;
            _environment = env;
        }



        [HttpPost("login")] // Ruta: POST /api/Propietarios/login
        public IActionResult Login([FromForm] string usuario, [FromForm] string clave)
        {
            Console.WriteLine(usuario);

            // Busca al propietario en la base de datos por su email
            var propietario = repoPropietarios.ObtenerPorEmail(usuario);

            // Hashea y compara la contraseña ingresada con la almacenada
            var hash = new PasswordHasher<Propietarios>();
            var res = hash.VerifyHashedPassword(propietario, propietario.password, clave);

            Console.WriteLine(propietario);

            // Si el usuario no existe o la contraseña no coincide, devuelve error 400
            if (propietario == null || res == PasswordVerificationResult.Failed)
                return BadRequest("Usuario o contraseña incorrectos");

            // Crea los datos (claims) que se incluirán dentro del token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, propietario.mail), // Guarda el email en el token
                new Claim("Id", propietario.idPropietario.ToString()) // Guarda el ID del propietario
            };

            // Obtiene la clave secreta y genera las credenciales de firma
            var secreto = _config["TokenAuthentication:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreto));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crea el token JWT con los claims, expiración y firma
            var token = new JwtSecurityToken(
                issuer: _config["TokenAuthentication:Issuer"],     // Quién emite el token
                audience: _config["TokenAuthentication:Audience"], // Quién puede usarlo
                claims: claims,                                    // Datos que contiene el token
                expires: DateTime.Now.AddHours(1),                 // Duración del token (1 hora)
                signingCredentials: creds                          // Firma digital
            );

            // Devuelve el token generado al cliente
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }



        [Authorize] // Requiere un token JWT válido
        [HttpGet]   // Ruta: GET /api/Propietarios
        public IActionResult GetPerfil()
        {
            // Obtiene el email del propietario autenticado (viene del token)
            var email = User.Identity.Name;

            // Busca en la base de datos al propietario correspondiente
            var propietario = repoPropietarios.ObtenerPorEmail(email);

            // Si no existe, devuelve 404
            if (propietario == null)
                return NotFound();

            // Devuelve los datos del propietario autenticado
            return Ok(propietario);
        }



        [Authorize] // Requiere un token válido
        [HttpPut("actualizar")] // Ruta: PUT /api/Propietarios/actualizar
        public IActionResult Actualizar([FromBody] Propietarios propietario)
        {
            // Obtiene el email del usuario autenticado (desde el token)
            var email = User.Identity.Name;

            // Busca los datos originales en la base de datos
            var original = repoPropietarios.ObtenerPorEmail(email);

            // Si no lo encuentra, devuelve 404
            if (original == null)
                return NotFound();

            // Mantiene el ID original (para no crear un nuevo propietario)
            original.dni = propietario.dni;
            original.nombre = propietario.nombre;
            original.apellido = propietario.apellido;
            original.telefono = propietario.telefono;
            original.mail = propietario.mail;

            // Llama al método del repositorio para actualizar los datos
            repoPropietarios.Actualizar(original);

            // Devuelve el objeto actualizado
            return Ok(original);
        }
    }
}
