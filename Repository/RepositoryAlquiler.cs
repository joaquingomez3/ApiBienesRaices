using System.Linq;
using ApiBienesRaices.Data;

using ApiBienesRaices.Repository.IRepository;

namespace ApiBienesRaices.Repository
{
    public class RepositoryAlquiler : IRepositoryAlquiler
    {
        private readonly AppDbContext contexto;

        public RepositoryAlquiler(AppDbContext contexto)
        {
            this.contexto = contexto;
        }

        public Alquiler ObtenerPorInmueble(int idInmueble)
        {
            return contexto.Alquiler
                .FirstOrDefault(a => a.idInmueble == idInmueble); // revisar
        }

        public int Alta(Alquiler alquiler)
        {
            contexto.Alquiler.Add(alquiler);
            return contexto.SaveChanges();
        }

        public int Actualizar(Alquiler alquiler)
        {
            contexto.Alquiler.Update(alquiler);
            return contexto.SaveChanges();
        }

        public int Eliminar(int id)
        {
            var contrato = contexto.Alquiler.FirstOrDefault(a => a.idAlquiler == id);
            if (contrato != null)
            {
                contexto.Alquiler.Remove(contrato);
                return contexto.SaveChanges();
            }
            return 0;
        }
    }
}
