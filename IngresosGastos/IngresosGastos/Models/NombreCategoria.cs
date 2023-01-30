using System.ComponentModel.DataAnnotations;

namespace IngresosGastos.Models
{
    public class NombreCategoria
    {
        // para mostrar el DropDownList: {Ingreso, Gasto}
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } // Ingreso o Gasto

    }
}
