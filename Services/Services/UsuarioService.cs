using Biblioteca_Guzman_Geovani.Context;
using Biblioteca_Guzman_Geovani.Models.Domain;
using Biblioteca_Guzman_Geovani.Services.IServices;

namespace Biblioteca_Guzman_Geovani.Services.Services
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
                List<Usuario> result = _context.Usuarios.ToList();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }
    }
}
