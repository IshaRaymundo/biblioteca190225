using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_Guzman_Geovani.Models.Domain
{
    public class Usuario
    {
        internal int PkUsuario;

        [Key]
        public int PkUssario { get; set; }
        public string Nombre { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        [ForeignKey("Roles")]
        public int FkRol { get; set; }

        public Rol Roles { get; set; }



    }
}
