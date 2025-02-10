using Biblioteca_Guzman_Geovani.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace Biblioteca_Guzman_Geovani.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
              new Usuario
              {
                  PkUssario = 1,
                  Nombre = "Geo",
                  UserName = "Usuario",
                  Password = "123",
                  FkRol = 1
              }
               );

            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    PkRol = 1,
                    Nombre = "Admin"
                });

        }
    }
}
