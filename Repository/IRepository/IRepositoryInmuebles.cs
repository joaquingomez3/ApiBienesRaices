using System.Collections.Generic;


namespace ApiBienesRaices.Repository.IRepository
{
    public interface IRepositoryInmuebles
    {
        IEnumerable<Inmuebles> ObtenerTodosPorPropietario(int idPropietario);
        IEnumerable<Inmuebles> ObtenerConContratoVigente(int idPropietario);
        int ObtenerPropietarioInmueble(int idInmueble);
        Inmuebles ObtenerPorId(int id);
        Inmuebles Agregar(Inmuebles inmueble);
        Inmuebles Actualizar(Inmuebles inmueble);
    }
}
