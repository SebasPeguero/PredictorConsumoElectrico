using System.ComponentModel.DataAnnotations;

namespace PredictorConsumoElectrico.Core.ViewModels
{
    public class ConsumoInputViewModel
    {
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime? Fecha { get; set; } // Inicializamos por defecto

        [Required(ErrorMessage = "El valor de consumo es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "No se permiten valores negativos")]
        public decimal? Valor { get; set; } // Inicializamos a 0
    }
}
