using Microsoft.EntityFrameworkCore;

namespace ApiBienesRaices.Data
{
    public class AppDbContext : DbContext //hereda de DbContext
    {
        //constructor que recibe opciones de configuración (cadena de conexión, proveedor MySQL, etc.)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Cada db set representa una tabla en la base de datos
        public DbSet<Propietarios> Propietarios { get; set; }
        public DbSet<Inmuebles> Inmuebles { get; set; }
        public DbSet<Inquilinos> Inquilinos { get; set; }
        public DbSet<Pagos> Pagos { get; set; }
        public DbSet<Alquiler> Alquiler { get; set; }
    }


}
