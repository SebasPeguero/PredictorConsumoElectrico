using PredictorConsumoElectrico.Core.DTOs;

namespace PredictorConsumoElectrico.Infrastructure
{
    public class PredictionSettings
    {
        //Recuerda el modo de predicción seleccionado por el usuario
        public string ModoSeleccionado { get; set; } = "SMA";

        //Recuerda las fechas y consumos guardados para cada predicción
        public List<ConsumoRecordDto> HistorialGuardado { get; set; } = new List<ConsumoRecordDto>();
    }
}
