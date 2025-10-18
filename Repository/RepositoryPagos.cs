using System.Collections.Generic;
using System.Linq;
using ApiBienesRaices.Data;

using ApiBienesRaices.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiBienesRaices.Repository
{
    public class RepositoryPagos : IRepositoryPagos
    {
        private readonly AppDbContext contexto;

        public RepositoryPagos(AppDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<List<Pagos>> ObtenerPorContrato(int idAlquiler)
        {
            // Implementación del endpoint: Obtener Pagos por Contrato
            // Devuelve una lista de todos los pagos de un alquiler específico.
            // Filtra la tabla Pagos usando el IdAlquiler (Id del Contrato).
            // .OrderBy() es opcional, pero ayuda a presentar los pagos en orden cronológico.
            return await contexto.Pagos
                                 .Where(p => p.idAlquiler == idAlquiler)
                                 .OrderBy(p => p.nroPago)
                                 .ThenBy(p => p.fecha)
                                 .ToListAsync();

        }


    }
}
