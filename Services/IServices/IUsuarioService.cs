using Biblioteca_Jonathan_Hernandez.Models.Domain;

namespace Biblioteca_Jonathan_Hernandez.Services.IServices
{
    public interface IUsuarioService
    {

        public List<Usuario> ObtenerUsuarios();

        public bool CrearUsuario(Usuario request);

        public Usuario GetUsuarioById(int id);
    }
}
