using System.Collections.Generic;


namespace ApiBienesRaices.Repository.IRepository
{
    public interface IRepositoryPagos
    {
        Task<List<Pagos>> ObtenerPorContrato(int idContrato);

    }
}
