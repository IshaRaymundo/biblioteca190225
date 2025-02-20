using Biblioteca_Mia_Raymundo.Context;
using Biblioteca_Mia_Raymundo.Models;
using Biblioteca_Mia_Raymundo.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Biblioteca_Mia_Raymundo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Users()
        {
            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        public IActionResult Config()
        {
            return View();
        }

        public IActionResult Libros()
        {
            return View();
        }

        public IActionResult Prestamos()
        {
            return View();
        }

        public IActionResult Reportes()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
