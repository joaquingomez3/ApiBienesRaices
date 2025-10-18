using ApiBienesRaices.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBienesRaices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlquilerController : ControllerBase
    {
        private readonly IRepositoryAlquiler repoAlquiler; // Acceso a los datos de inmuebles
        private readonly IConfiguration _config;                   // Acceso a appsettings.json
        private readonly IWebHostEnvironment _environment;          // Para trabajar con archivos y rutas físicas

        // Constructor con inyección de dependencias
        public AlquilerController(IRepositoryAlquiler repo, IConfiguration config, IWebHostEnvironment env)
        {
            repoAlquiler = repo;
            _config = config;
            _environment = env;
        }

        // Obtener Contrato por Inmueble
        [HttpGet("inmueble/{id}")]// Ruta: GET /api/Alquiler/inmueble/{id}
        public async Task<ActionResult<Alquiler>> contratosPorInmueble(int id)
        {
            var contrato = await repoAlquiler.ObtenerPorInmueble(id);

            if (contrato == null)
            {
                return NotFound($"No se encontró un contrato activo para el inmueble con ID {id}.");
            }




            return Ok(contrato);
        }



    }



}