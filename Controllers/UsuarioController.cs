using Biblioteca_Mia_Raymundo.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Biblioteca_Mia_Raymundo.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca_Mia_Raymundo.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Biblioteca_Mia_Raymundo.Context;

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

            // Si la contraseña ha sido modificada, la hasheamos
            if (!string.IsNullOrEmpty(request.Password))
            {
                var passwordHasher = new PasswordHasher<Usuario>();
                request.Password = passwordHasher.HashPassword(request, request.Password); // Hashear la nueva contraseña
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

        //FUNCIONALIDADES DE REGITRO
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Registro")]
        public IActionResult RegistroPost(RegisterViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var usuario = new Usuario
            {
                Nombre = model.Nombre,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                FkRol = 1
            };
            bool resultado = _usuarioService.CrearUsuario(usuario);
            if (resultado)
            {
                return RedirectToAction("Login", "Usuario");

            }
            ModelState.AddModelError("", "No se pudo registrar el usuario");
            return View(model);
        }
        //FUNCIONALIDADES DE LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = _usuarioService.ObtenerUsuarios()
                .FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

            if (usuario == null)
            {
                return Unauthorized(new { Message = "Usuario no encontrado." });
            }

            var passwordHasher = new PasswordHasher<Usuario>();
            var resultadoVerificacion = passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

            if (resultadoVerificacion != PasswordVerificationResult.Success)
            {
                return Unauthorized(new { Message = "Contraseña incorrecta." });
            }

            // Leer configuraciones JWT desde appsettings.json usando IConfiguration
            var jwtSettings = HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var secretKey = jwtSettings["JwtSettings:SecretKey"];
            var issuer = jwtSettings["JwtSettings:Issuer"];
            var audience = jwtSettings["JwtSettings:Audience"];
            var expiryInMinutes = int.Parse(jwtSettings["JwtSettings:ExpiryInMinutes"]);

            // Crear claims para el token
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
        new Claim("UsuarioId", usuario.PkUsuario.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            Response.Cookies.Append("AuthToken", tokenString, new CookieOptions
            {
                HttpOnly = true, // Evita acceso desde JavaScript (seguridad)
                Secure = true, // Solo se envía en HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes)
            });
            // Retornar el token en formato JSON
            return RedirectToAction("Index", "Usuario");
        }
        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            // Asegurar que la cookie AuthToken se elimina correctamente
            Response.Cookies.Append("AuthToken", "", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1), // Fecha de expiración en el pasado
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            // Redireccionar a la página de inicio de sesión u otra página
            return RedirectToAction("Login", "Usuario");
        }

    }


}
