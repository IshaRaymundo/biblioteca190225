using Biblioteca_Mia_Raymundo.Models.Domain;

namespace Biblioteca_Mia_Raymundo.Services.IServices
{
    public interface ILibroService
    {
        List<Libro> ObtenerLibros();
        Libro GetLibroById(int id);
        bool CrearLibro(Libro request);
        bool ActualizarLibro(int id, Libro request);
        bool EliminarLibro(int id);
    }
}
