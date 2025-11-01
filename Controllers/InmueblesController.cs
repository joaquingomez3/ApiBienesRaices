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
    [Authorize]
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

        //GET: /api/Inmuebles/GetContratoVigente
        [HttpGet("GetContratoVigente")]
        public IActionResult ObtenerConContratoVigente()
        {
            try
            {
                var idPropClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (idPropClaim == null)
                    return Unauthorized("Token inválido o no proporcionado");
                int idPropietario = int.Parse(idPropClaim.Value);
                var lista = repoInmuebles.ObtenerConContratoVigente(idPropietario);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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

                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imagen.CopyTo(fileStream);
                    }
                    var baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";

                    inmuebleData.imagen = $"{baseUrl}/uploads/{fileName}";
                    inmuebleData.imagenLocal = filePath;
                }
                else
                {
                    inmuebleData.imagen = "https://placehold.co/600x400";
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
        public IActionResult Actualizar([FromBody] Inmuebles inmuebleData)
        {
            try
            {
                var idPropClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (idPropClaim == null)
                    return Unauthorized("Token inválido o no proporcionado");

                int idPropietario = int.Parse(idPropClaim.Value);
                int idPropietarioActual = repoInmuebles.ObtenerPropietarioInmueble(inmuebleData.idInmueble);

                if (idPropietario != idPropietarioActual)
                    return Unauthorized("No tiene permiso para actualizar este inmueble.");

                inmuebleData.idPropietario = idPropietarioActual;

                var actualizado = repoInmuebles.Actualizar(inmuebleData);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }




}