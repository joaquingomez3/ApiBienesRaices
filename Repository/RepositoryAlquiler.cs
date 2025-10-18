using System.Linq;
using ApiBienesRaices.Data;

using ApiBienesRaices.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiBienesRaices.Repository
{
    public class RepositoryAlquiler : IRepositoryAlquiler
    {
        private readonly AppDbContext contexto;

        public RepositoryAlquiler(AppDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Alquiler> ObtenerPorInmueble(int idInmueble)
        {

            return await contexto.Alquiler
                                  .Include(a => a.Inquilino)
                                  .Include(a => a.Inmueble)
                                  .FirstOrDefaultAsync(a => a.idInmueble == idInmueble);

        }
    }
}
