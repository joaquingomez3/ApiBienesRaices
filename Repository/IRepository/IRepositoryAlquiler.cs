

namespace ApiBienesRaices.Repository.IRepository
{
    public interface IRepositoryAlquiler
    {
        Alquiler ObtenerPorInmueble(int idInmueble);
        int Alta(Alquiler contrato);
        int Actualizar(Alquiler contrato);
        int Eliminar(int id);
    }
}
