using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace PredictorConsumoElectrico.Core.ViewModels
{
    public class HomeViewModel
    {
        public List <ConsumoInputViewModel> Registros { get; set; }

        public decimal? ConsumoProximoMes { get; set; }
        public string? tendencia { get; set; }

        public string? NombreMetodoSeleccionado { get; set; }

        public string? MensajeDetallado { get; set; }

        public bool HayResultado { get; set; }

        public HomeViewModel()
        {
            Registros = new List<ConsumoInputViewModel>();
            
            for (int i = 0; i < 12; i++)
            {
                Registros.Add(new ConsumoInputViewModel());
            }
        }
    }
}
