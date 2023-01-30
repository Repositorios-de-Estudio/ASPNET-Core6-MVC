using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IngresosGastos.Models
{
    public class IngresoGasto
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; } // Categoria de la clase/tabla Categoria

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; } //cuando se ingreso el ingreso o gasto

        [Required]
        [Range(1, 100000)] // para que siempre sea mayor que 1
        //[DisplayFormat(DataFormatString = "{0:C}")] //formato moneda
        [Display(Name = "Valor")]
        public double Valor { get; set; }
    }
}
