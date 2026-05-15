using System;
using System.Collections.Generic;
using System.Text;

namespace PredictorConsumoElectrico.Core.DTOs
{
    public class PredictionResultDto
    {
        public required decimal ConsumoProximoMes { get; set; }
        public required string Tendencia { get; set; }
        public required string NombreMetodo { get; set; }
        public required string?  MensajeDetallado { get; set ; }
    }
}
