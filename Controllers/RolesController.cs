using Biblioteca_Mia_Raymundo.Models.Domain;
using Biblioteca_Mia_Raymundo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Biblioteca_Mia_Raymundo.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRolService _rolService;

        public RolesController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _rolService.ObtenerTodosLosRolesAsync();
            return View(roles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var rol = await _rolService.ObtenerRolPorIdAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // GET: Roles/Create
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Nombre")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                await _rolService.CrearRolAsync(rol);
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var rol = await _rolService.ObtenerRolPorIdAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return View(rol);
            }

            await _rolService.ActualizarRolAsync(rol);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Roles/Delete/{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            var eliminado = await _rolService.EliminarRolAsync(id);
            if (eliminado)
            {
                return Json(new { success = true, message = "Rol eliminado correctamente." });
            }
            return Json(new { success = false, message = "Error al eliminar el rol." });
        }


    }
}
