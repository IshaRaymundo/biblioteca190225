using Biblioteca_Jonathan_Hernandez.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace Biblioteca_Jonathan_Hernandez.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        //modelos a mapear
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primero insertar los Roles
            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    PkRol = 1,
                    Nombre = "sa"
                }
            );

            // Luego insertar los Usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    PkUsuario = 1,
                    Nombre = "Jonny",
                    UserName = "Usuario",
                    Password = "123",
                    FkRol = 1  // Se asegura de que ya exista el Rol con ID 1
                }
            );
        }


    }
}
