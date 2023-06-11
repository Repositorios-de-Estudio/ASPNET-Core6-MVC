using System.ComponentModel.DataAnnotations;

namespace IngresosGastos.Models
{
    // Categoria ingresos o gastos
    public class Categoria
    {
        // estas anotaciones sirven para cuando se cree la BD y tabla Categoria se usen como campos de la tabla
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name ="Nombre Categoria")] // asi se muestra en la vista
        public string NombreCategoria { get; set; }

        [Required]
        [MaxLength(2)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } // IN: Ingreso, GA: Gasto

        [Required]
        public bool Estado { get; set; }
    }
}
