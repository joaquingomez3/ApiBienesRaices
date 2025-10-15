using System.Collections.Generic;
using System.Linq;
using ApiBienesRaices.Data;

using ApiBienesRaices.Repository.IRepository;

namespace ApiBienesRaices.Repository
{
    public class RepositoryInmuebles : IRepositoryInmuebles
    {
        private readonly AppDbContext contexto;

        public RepositoryInmuebles(AppDbContext contexto)
        {
            this.contexto = contexto;
        }

        public List<Inmuebles> ObtenerPorPropietario(int idPropietario)
        {
            return contexto.Inmuebles
                .Where(i => i.idPropietario == idPropietario)
                .ToList();
        }

        public List<Inmuebles> ObtenerConContratoVigente(int idPropietario)
        {
            return contexto.Inmuebles
                .Where(i => i.idPropietario == idPropietario && i.disponible == false)
                .ToList();
        }

        public Inmuebles ObtenerPorId(int id)
        {
            return contexto.Inmuebles.FirstOrDefault(i => i.idInmueble == id);
        }

        public int Alta(Inmuebles inmueble)
        {
            contexto.Inmuebles.Add(inmueble);
            return contexto.SaveChanges();
        }

        public int Actualizar(Inmuebles inmueble)
        {
            contexto.Inmuebles.Update(inmueble);
            return contexto.SaveChanges();
        }

        public int Eliminar(int id)
        {
            var inmueble = ObtenerPorId(id);
            if (inmueble != null)
            {
                contexto.Inmuebles.Remove(inmueble);
                return contexto.SaveChanges();
            }
            return 0;
        }
    }
}
