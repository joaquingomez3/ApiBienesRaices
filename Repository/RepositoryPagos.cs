using System.Collections.Generic;
using System.Linq;
using ApiBienesRaices.Data;

using ApiBienesRaices.Repository.IRepository;

namespace ApiBienesRaices.Repository
{
    public class RepositoryPagos : IRepositoryPagos
    {
        private readonly AppDbContext contexto;

        public RepositoryPagos(AppDbContext contexto)
        {
            this.contexto = contexto;
        }

        public List<Pagos> ObtenerPorContrato(int idAlquiler)
        {
            return contexto.Pagos
                .Where(p => p.idAlquiler == idAlquiler)
                .ToList();
        }

        public int Alta(Pagos pago)
        {
            contexto.Pagos.Add(pago);
            return contexto.SaveChanges();
        }

        public int Actualizar(Pagos pago)
        {
            contexto.Pagos.Update(pago);
            return contexto.SaveChanges();
        }

        public int Eliminar(int id)
        {
            var pago = contexto.Pagos.FirstOrDefault(p => p.idPago == id);
            if (pago != null)
            {
                contexto.Pagos.Remove(pago);
                return contexto.SaveChanges();
            }
            return 0;
        }
    }
}
