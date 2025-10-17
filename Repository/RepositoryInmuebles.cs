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

        public IEnumerable<Inmuebles> ObtenerConContratoVigente(int idPropietario)
        {
            var hoy = DateTime.Now;
            var inmuebles = contexto.Inmuebles //representa la tabla Inmuebles
                .Include(i => i.Alquileres)//incluye los alquileres relacionados con el inmueble
                .Where(i => i.idPropietario == idPropietario //filtro por propietario
                    && i.Alquileres.Any(a => a.fecha_inicio <= hoy && a.fechaFin >= hoy))//filtro por contrato vigente dentro del rango de fechas
                .ToList();
            return inmuebles;
        }

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
