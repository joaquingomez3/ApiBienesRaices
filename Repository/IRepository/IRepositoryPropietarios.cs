using System;
namespace ApiBienesRaices.Repository.IRepository;


public interface IRepositoryPropietarios
{
    Propietarios ObtenerPorEmail(string mail);
    Propietarios ObtenerPorId(int id);
    List<Propietarios> ObtenerTodos();

    Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword);
    int Alta(Propietarios propietario);
    int Actualizar(Propietarios propietario);
    int Eliminar(int id);
}
