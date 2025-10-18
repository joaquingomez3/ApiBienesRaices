using ApiBienesRaices.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBienesRaices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PagosController : ControllerBase
    {
        private readonly IRepositoryPagos repoPagos; // Acceso a los datos de pagos
        private readonly IConfiguration _config;                   // Acceso a appsettings.json
        private readonly IWebHostEnvironment _environment;          // Para trabajar con archivos y rutas físicas

        // Constructor con inyección de dependencias
        public PagosController(IRepositoryPagos repo, IConfiguration config, IWebHostEnvironment env)
        {
            repoPagos = repo;
            _config = config;
            _environment = env;
        }

        //  Obtener Pagos por Contrato
        [HttpGet("contrato/{id}")] // Ruta: GET /api/Pagos/contrato/{id}
        public async Task<ActionResult<IEnumerable<Pagos>>> GetPagosByContrato(int id)
        {
            var pagos = await repoPagos.ObtenerPorContrato(id);

            if (!pagos.Any())
            {
                return NotFound($"No se encontraron pagos registrados para el contrato con ID {id}.");
            }



            return Ok(pagos);
        }

    }

}