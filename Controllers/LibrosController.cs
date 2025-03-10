using Biblioteca_Mia_Raymundo.Models.Domain;
using Biblioteca_Mia_Raymundo.Services.IServices;
using Microsoft.AspNetCore.Mvc;

public class LibrosController : Controller
{
    private readonly ILibroService _libroService;

    public LibrosController(ILibroService libroService)
    {
        _libroService = libroService;
    }

    // Obtener todos los libros
    [HttpGet]
    public IActionResult Index() 
    {
        var libros = _libroService.ObtenerLibros();
        return View(libros); // Devuelve la vista con los libros
    }

    // Crear un nuevo libro
    [HttpGet]
    public IActionResult Create()
    {
        return View(); 
    }

    // Crear libro - Guardar
    [HttpPost]
    public IActionResult Create(Libro libro)
    {
        if (ModelState.IsValid)
        {
            _libroService.CrearLibro(libro);
            return RedirectToAction("Index");
        }
        return View(libro);
    }

    // Editar un libro - Formulario
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var libro = _libroService.GetLibroById(id);
        if (libro == null)
        {
            return NotFound();
        }
        return View(libro); 
    }

    // Editar libro - Guardar cambios
    [HttpPost]
    public IActionResult Edit(int id, Libro libro)
    {
        if (id != libro.PkLibro)
        {
            return BadRequest();
        }

        if (_libroService.ActualizarLibro(id, libro))
        {
            return RedirectToAction("Index"); 
        }

        return View(libro); 
    }

    // Eliminar un libro
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var libro = _libroService.GetLibroById(id);
        if (libro == null)
        {
            return NotFound();
        }

        if (_libroService.EliminarLibro(id))
        {
            return Ok(new { success = true, message = "Libro eliminado correctamente." });
        }

        return BadRequest(new { success = false, message = "No se pudo eliminar el libro." });
    }


}
