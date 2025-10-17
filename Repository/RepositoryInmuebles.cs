using System.Collections.Generic;
using System.Linq;
using ApiBienesRaices.Data;

using ApiBienesRaices.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiBienesRaices.Repository
{
    public class RepositoryInmuebles : IRepositoryInmuebles
    {
        private readonly AppDbContext contexto;

        public RepositoryInmuebles(AppDbContext contexto)
        {
            this.contexto = contexto;
        }

        public IEnumerable<Inmuebles> ObtenerTodosPorPropietario(int idPropietario)
        {
            return contexto.Inmuebles
                .Include(i => i.Propietario)
                .Where(i => i.idPropietario == idPropietario)
                .ToList();
        }

        // public IEnumerable<Inmuebles> ObtenerConContratoVigente(int idPropietario)
        // {
        //     var hoy = DateTime.Now.Date;
        //     return contexto.Inmuebles
        //         .Include(i => i.disponible)
        //         .Where(i => i.idPropietario == idPropietario &&
        //                     i..Any(c => c.FechaInicio <= hoy && c.FechaFin >= hoy))
        //         .ToList();
        // }

        public Inmuebles ObtenerPorId(int id)
        {
            return contexto.Inmuebles
                .Include(i => i.Propietario)
                .FirstOrDefault(i => i.idInmueble == id);
        }

        public Inmuebles Agregar(Inmuebles inmueble)
        {
            contexto.Inmuebles.Add(inmueble);
            contexto.SaveChanges();
            return inmueble;
        }

        public Inmuebles Actualizar(Inmuebles inmueble)
        {
            contexto.Inmuebles.Update(inmueble);
            contexto.SaveChanges();
            return inmueble;
        }
    }
}
