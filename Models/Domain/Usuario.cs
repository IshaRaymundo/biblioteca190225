using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_Jonathan_Hernandez.Models.Domain
{
    public class Usuario
    {
        [Key]
        public int PkUsuario { get; set; }  // Mantén solo esta definición

        public string Nombre { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [ForeignKey("Roles")]
        public int FkRol { get; set; }

        public Rol Roles { get; set; }
    }
}
