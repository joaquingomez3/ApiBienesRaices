using System.Collections.Generic;


namespace ApiBienesRaices.Repository.IRepository
{
    public interface IRepositoryInmuebles
    {
        List<Inmuebles> ObtenerPorPropietario(int idPropietario);
        List<Inmuebles> ObtenerConContratoVigente(int idPropietario);
        Inmuebles ObtenerPorId(int id);
        int Alta(Inmuebles inmueble);
        int Actualizar(Inmuebles inmueble);
        int Eliminar(int id);
    }
}
