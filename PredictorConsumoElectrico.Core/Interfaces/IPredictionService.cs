using System;
using System.Collections.Generic;
using System.Text;
using PredictorConsumoElectrico.Core.DTOs;

namespace PredictorConsumoElectrico.Core.Interfaces
{
    public interface IPredictionService
    {

        PredictionResultDto Predecir(List<ConsumoRecordDto> datosHistorial);

    }
}
