using Biblioteca_Jonathan_Hernandez.Context;
using Biblioteca_Jonathan_Hernandez.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Biblioteca_Jonathan_Hernandez.Services.IServices;

namespace Biblioteca_Jonathan_Hernandez.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;
        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        //funcion para obtener lista de usuarios
        public List<Usuario> ObtenerUsuarios()
        {
        
            try
            {
                List<Usuario> result = _context.Usuarios.Include(x => x.Roles).ToList();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        //Funcion para obtener un Usuario
        public Usuario GetUsuarioById(int id)
        {
            try
            {
                Usuario result = _context.Usuarios.Find(id);

                if (result == null)
                {
                    throw new Exception("Usuario no encontrado.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        //Funcion para crear usuario
        public bool CrearUsuario( Usuario request)
        {
            try
            {
                Usuario usuario = new Usuario()
                {
                    Nombre = request.Nombre,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRol = 1
                };

                _context.Usuarios.Add(usuario);
               int result = _context.SaveChanges();

                if (result > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message);
            }
        }

    }
}
