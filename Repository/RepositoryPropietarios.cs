using System;
using ApiBienesRaices.Data;
using ApiBienesRaices.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiBienesRaices.Repository;

public class RepositoryPropietarios : IRepositoryPropietarios
{
    private readonly AppDbContext contexto;  //creo instancia del contexto
    private string? secretKey;

    public RepositoryPropietarios(AppDbContext db, IConfiguration configuration)  //constructor
    {
        contexto = db;
        secretKey = configuration.GetValue<string>("TokenAuthentication:SecretKey");
    }

    public Propietarios ObtenerPorEmail(string mail)
    {
        return contexto.Propietarios.FirstOrDefault(p => p.mail == mail);
    }

    public Propietarios ObtenerPorId(int id)
    {
        return contexto.Propietarios.FirstOrDefault(p => p.idPropietario == id);
    }

    public List<Propietarios> ObtenerTodos()
    {
        return contexto.Propietarios.ToList();
    }

    public int Alta(Propietarios propietario)
    {
        contexto.Propietarios.Add(propietario);
        return contexto.SaveChanges();
    }

    public int Actualizar(Propietarios propietario)
    {
        contexto.Propietarios.Update(propietario);
        return contexto.SaveChanges();
    }

    public int Eliminar(int id)
    {
        var propietario = ObtenerPorId(id);
        if (propietario != null)
        {
            contexto.Propietarios.Remove(propietario);
            return contexto.SaveChanges();
        }
        return 0;
    }

    public async Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
    {
        var _passwordHasher = new PasswordHasher<Propietarios>();
        // Buscar propietario autenticado
        var propietario = await contexto.Propietarios.FirstOrDefaultAsync(p => p.mail == email);
        if (propietario == null)
            return false;

        // Verificar contraseña actual
        var verificationResult = _passwordHasher.VerifyHashedPassword(propietario, propietario.password, currentPassword);
        if (verificationResult == PasswordVerificationResult.Failed)
            return false;

        // Hashear nueva contraseña
        propietario.password = _passwordHasher.HashPassword(propietario, newPassword);

        // Guardar cambios
        await contexto.SaveChangesAsync();

        return true;
    }

}
