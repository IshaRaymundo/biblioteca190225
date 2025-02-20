using Biblioteca_Mia_Raymundo.Models.Domain;

namespace Biblioteca_Mia_Raymundo.Services.IServices
{
    public interface IUsuarioService
    {

        public List<Usuario> ObtenerUsuarios();

        public bool CrearUsuario(Usuario request);

        public Usuario GetUsuarioById(int id);
        public bool ActualizarUsuario(int id, Usuario request);
        public bool EliminarUsuario(int id);
    }
}
