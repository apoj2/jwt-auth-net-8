using Backend_dotnet8.Core.Entities;
using Backend_dotnet8.Core.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend_dotnet8.Core.DbContext
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<CajaRegistro> CajasRegistros { get; set; }
        public DbSet<CategoriaProducto> CategoriaProductos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Negocio> Negocio { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(element =>
            {
                element.ToTable("Users");
            });

            builder.Entity<IdentityUserClaim<string>>(element =>
            {
                element.ToTable("UserClaims");
            });


            builder.Entity<IdentityUserLogin<string>>(element =>
            {
                element.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserToken<string>>(element =>
            {
                element.ToTable("UserTokens");
            });
            builder.Entity<IdentityRole>(element =>
            {
                element.ToTable("Roles");
            });
            builder.Entity<IdentityRoleClaim<string>>(element =>
            {
                element.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserRole<string>>(element =>
            {
                element.ToTable("UserRoles");
            });



        }

    }
}
