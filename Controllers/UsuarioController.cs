using Biblioteca_Mia_Raymundo.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Biblioteca_Mia_Raymundo.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Mia_Raymundo.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Listar usuarios
        public IActionResult Index()
        {
            var result = _usuarioService.ObtenerUsuarios();
            return View(result);
        }

        // Crear usuario - Formulario
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // Crear usuario - Guardar
        [HttpPost]
        public IActionResult Crear(Usuario request)
        {
            _usuarioService.CrearUsuario(request);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var usuario = _usuarioService.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }


        // Editar usuario - Guardar cambios
        [HttpPost]
        public IActionResult Editar(int id, Usuario request)
        {
            if (id != request.PkUsuario)
            {
                return BadRequest();
            }

            bool actualizado = _usuarioService.ActualizarUsuario(id, request);
            if (!actualizado)
            {
                return View(request);
            }

            TempData["UsuarioEditado"] = true; // Flag para mostrar el modal
            return RedirectToAction("Index");
        }

        // Eliminar usuario - Confirmar eliminación
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var usuario = _usuarioService.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // Eliminar usuario - Ejecutar eliminación
        [HttpDelete]
        public JsonResult EliminarUsuario(int id)
        {
            Console.WriteLine($"🔵 Intentando eliminar usuario con ID: {id}");

            bool eliminado = _usuarioService.EliminarUsuario(id);

            if (!eliminado)
            {
                Console.WriteLine($"❌ No se pudo eliminar el usuario con ID: {id}");
                return Json(new { success = false, message = "No se pudo eliminar el usuario." });
            }

            Console.WriteLine($"🟢 Usuario con ID: {id} eliminado correctamente.");
            return Json(new { success = true, message = "Usuario eliminado correctamente." });
        }


    }
}
