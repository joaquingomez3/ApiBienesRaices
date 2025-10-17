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
    public class InmueblesController : ControllerBase
    {
        // Dependencias inyectadas
        private readonly IRepositoryInmuebles repoInmuebles; // Acceso a los datos de inmuebles
        private readonly IConfiguration _config;                   // Acceso a appsettings.json
        private readonly IWebHostEnvironment _environment;          // Para trabajar con archivos y rutas físicas

        // Constructor con inyección de dependencias
        public InmueblesController(IRepositoryInmuebles repo, IConfiguration config, IWebHostEnvironment env)
        {
            repoInmuebles = repo;
            _config = config;
            _environment = env;
        }


        // GET: /api/Inmuebles
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            try
            {
                var idPropClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (idPropClaim == null)
                    return Unauthorized("Token inválido o no proporcionado");
                int idPropietario = int.Parse(idPropClaim.Value);
                var lista = repoInmuebles.ObtenerTodosPorPropietario(idPropietario);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: /api/Inmuebles/GetContratoVigente
        // [HttpGet("GetContratoVigente")]
        // public IActionResult ObtenerConContratoVigente()
        // {
        //     try
        //     {
        //         int idPropietario = ObtenerIdPropietarioDesdeToken();
        //         var lista = repoInmuebles.ObtenerConContratoVigente(idPropietario);
        //         return Ok(lista);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        // POST: /api/Inmuebles/cargar
        [HttpPost("cargar")]
        public IActionResult CargarInmueble([FromForm] IFormFile? imagen, [FromForm] string inmueble)
        {
            try
            {
                var inmuebleData = System.Text.Json.JsonSerializer.Deserialize<Inmuebles>(inmueble);//conviere la cadena json a objeto inmueble
                var idPropClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (idPropClaim == null)
                    return Unauthorized("Token inválido o no proporcionado");
                int idPropietario = int.Parse(idPropClaim.Value);
                inmuebleData.idPropietario = idPropietario;

                if (imagen != null && imagen.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imagen.CopyTo(stream);
                    }

                    inmuebleData.imagen = "/uploads/" + fileName;
                }

                var nuevo = repoInmuebles.Agregar(inmuebleData);
                return CreatedAtAction(nameof(ObtenerTodos), new { id = nuevo.idInmueble }, nuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: /api/Inmuebles/actualizar
        [HttpPut("actualizar")]
        public IActionResult Actualizar([FromBody] Inmuebles inmueble)
        {
            try
            {
                var idPropClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (idPropClaim == null)
                    return Unauthorized("Token inválido o no proporcionado");
                int idPropietario = int.Parse(idPropClaim.Value);
                inmueble.idPropietario = idPropietario;

                var actualizado = repoInmuebles.Actualizar(inmueble);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }



}