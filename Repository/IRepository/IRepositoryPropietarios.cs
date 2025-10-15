using System;
namespace ApiBienesRaices.Repository.IRepository;


public interface IRepositoryPropietarios
{
    Propietarios ObtenerPorEmail(string mail);
    Propietarios ObtenerPorId(int id);
    List<Propietarios> ObtenerTodos();
    int Alta(Propietarios propietario);
    int Actualizar(Propietarios propietario);
    int Eliminar(int id);
}
