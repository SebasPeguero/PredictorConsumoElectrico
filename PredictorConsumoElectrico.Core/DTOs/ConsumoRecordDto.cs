using System;
using System.Collections.Generic;
using System.Text;

namespace PredictorConsumoElectrico.Core.DTOs
{
    public class ConsumoRecordDto
    {
        public required DateTime Fecha { get; set; }

        public required decimal Consumo { get; set; }
    }
}
