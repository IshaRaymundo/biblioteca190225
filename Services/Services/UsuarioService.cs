using Biblioteca_Mia_Raymundo.Context;
using Biblioteca_Mia_Raymundo.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Biblioteca_Mia_Raymundo.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Biblioteca_Mia_Raymundo.Services.Services
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
        //public bool CrearUsuario( Usuario request)
        //{
        //    try
        //    {
        //        Usuario usuario = new Usuario()
        //        {
        //            Nombre = request.Nombre,
        //            UserName = request.UserName,
        //            Password = request.Password,
        //            FkRol = 1
        //        };

        //        _context.Usuarios.Add(usuario);
        //       int result = _context.SaveChanges();

        //        if (result > 0)
        //        {
        //            return true;
        //        }
        //        return false;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Sucedio un error: " + ex.Message);
        //    }
        //}
        public bool CrearUsuario(Usuario request)
        {

            try
            {
                var passwordHasher = new PasswordHasher<Usuario>();

                Usuario usuario = new Usuario()
                {
                    Nombre = request.Nombre,
                    UserName = request.UserName,
                    Password = passwordHasher.HashPassword(request, request.Password),
                    Email = request.Email,
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
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception("Sucedió un error: " + errorMessage);
            }


        }

        // Actualizar un usuario existente
        public bool ActualizarUsuario(int id, Usuario request)
        {
            try
            {
                var usuario = _context.Usuarios.Find(id);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado.");
                }

                usuario.Nombre = request.Nombre;
                usuario.UserName = request.UserName;
                usuario.Password = request.Password;

                _context.Usuarios.Update(usuario);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        // Eliminar un usuario
        public bool EliminarUsuario(int id)
        {
            try
            {
                var usuario = _context.Usuarios.Find(id);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado.");
                }

                _context.Usuarios.Remove(usuario);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

    }
}
