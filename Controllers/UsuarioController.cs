using Biblioteca_Jonathan_Hernandez.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Biblioteca_Jonathan_Hernandez.Models.Domain;

namespace Biblioteca_Jonathan_Hernandez.Controllers
{
    public class UsuarioController : Controller
    {
        private  readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService) { 

            _usuarioService = usuarioService;
        }


        public IActionResult Index()
        {
            var result = _usuarioService.ObtenerUsuarios();
            return View(result);
        }


        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Crear(Usuario request)
        {
            _usuarioService.CrearUsuario(request);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            var result = _usuarioService.GetUsuarioById(id);
            return View();
        }
    }
}
