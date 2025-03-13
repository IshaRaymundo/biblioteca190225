using System.ComponentModel.DataAnnotations;

namespace Biblioteca_Mia_Raymundo.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es requerido.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}