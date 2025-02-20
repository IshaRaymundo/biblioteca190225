using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_Mia_Raymundo.Models.Domain
{
    public class Rol
    {

        [Key]
        public int PkRol { get; set; }

        public string Nombre { get; set; }



    }
}
