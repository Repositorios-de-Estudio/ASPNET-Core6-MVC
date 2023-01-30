﻿using System.ComponentModel.DataAnnotations;

namespace IngresosGastos.Models
{
    // Categoria ingresos o gastos
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name ="Nombre Categoria")]
        public string NombreCategoria { get; set; }

        [Required]
        [MaxLength(2)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } // IN: Ingreso, GA: Gasto

        [Required]
        public bool Estado { get; set; }
    }
}
