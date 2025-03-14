﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_Mia_Raymundo.Models.Domain
{
    public class Usuario
    {
        [Key]
        public int PkUsuario { get; set; }  // Mantén solo esta definición

        public string Nombre { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [ForeignKey("Roles")]
        public int FkRol { get; set; }

        public Rol Roles { get; set; }
    }
}
