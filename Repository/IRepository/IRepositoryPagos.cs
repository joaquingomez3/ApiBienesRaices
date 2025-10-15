using System.Collections.Generic;


namespace ApiBienesRaices.Repository.IRepository
{
    public interface IRepositoryPagos
    {
        List<Pagos> ObtenerPorContrato(int idContrato);
        int Alta(Pagos pago);
        int Actualizar(Pagos pago);
        int Eliminar(int id);
    }
}
