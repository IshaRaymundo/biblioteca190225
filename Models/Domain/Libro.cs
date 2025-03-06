using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_Mia_Raymundo.Models.Domain
{
    public class Libro
    {
        [Key]
        public int PkLibro { get; set; }

        [Required(ErrorMessage = "El nombre del libro es obligatorio.")]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [StringLength(255)]
        public string Autor { get; set; }

        [Range(1000, 2100, ErrorMessage = "El año debe estar entre 1000 y 2100.")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public string Genero { get; set; }
    }
}
