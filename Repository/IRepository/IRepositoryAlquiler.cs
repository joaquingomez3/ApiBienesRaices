

namespace ApiBienesRaices.Repository.IRepository
{
    public interface IRepositoryAlquiler
    {
        Task<Alquiler> ObtenerPorInmueble(int idInmueble);

    }
}
