using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_Jonathan_Hernandez.Models.Domain
{
    public class Rol
    {

        [Key]
        public int PkRol { get; set; }

        public string Nombre { get; set; }



    }
}
