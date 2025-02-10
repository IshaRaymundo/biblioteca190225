using Biblioteca_Guzman_Geovani.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Guzman_Geovani.Controllers
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
    }
}
