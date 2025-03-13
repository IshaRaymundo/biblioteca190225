using Biblioteca_Mia_Raymundo.Context;
using Biblioteca_Mia_Raymundo.Models.Domain;
using Biblioteca_Mia_Raymundo.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Mia_Raymundo.Services.Services
{
    public class LibroService : ILibroService
    {
        private readonly ApplicationDbContext _context;

        public LibroService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener lista de libros
        public List<Libro> ObtenerLibros()
        {
            try
            {
                return _context.Libros.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        // Obtener un libro por ID
        public Libro GetLibroById(int id)
        {
            try
            {
                var libro = _context.Libros.Find(id);
                if (libro == null)
                {
                    throw new Exception("Libro no encontrado.");
                }
                return libro;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        // Crear un nuevo libro
        public bool CrearLibro(Libro request)
        {
            try
            {
                Libro libro = new Libro()
                {
                    Nombre = request.Nombre,
                    Autor = request.Autor,
                    Anio = request.Anio,
                    PkGenero = request.PkGenero
                };

                _context.Libros.Add(libro);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        // Actualizar un libro existente
        public bool ActualizarLibro(int id, Libro request)
        {
            try
            {
                var libro = _context.Libros.Find(id);
                if (libro == null)
                {
                    throw new Exception("Libro no encontrado.");
                }

                libro.Nombre = request.Nombre;
                libro.Autor = request.Autor;
                libro.Anio = request.Anio;
                libro.PkGenero = request.PkGenero;

                _context.Libros.Update(libro);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        // Eliminar un libro
        public bool EliminarLibro(int id)
        {
            try
            {
                var libro = _context.Libros.Find(id);
                if (libro == null)
                {
                    throw new Exception("Libro no encontrado.");
                }

                _context.Libros.Remove(libro);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }
    }
}
