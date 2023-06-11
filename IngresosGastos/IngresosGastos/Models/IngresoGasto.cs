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

        //relaciona la tabla categoria con esta tabla, trae las categorias a esta tabla
        [ForeignKey("CategoriaId")] 
        public Categoria? Categoria { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Range(1, 10000000000)]
        [DisplayFormat(DataFormatString = "{0:C}")] //C: currency
        [Display(Name = "Valor")]
        public double Valor { get; set; }

    }
}
