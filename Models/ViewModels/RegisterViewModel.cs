using System.ComponentModel.DataAnnotations;

namespace Biblioteca_Mia_Raymundo.Models.ViewModels



{
    public class RegisterViewModels
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El username es requerido.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El email es requerido.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}