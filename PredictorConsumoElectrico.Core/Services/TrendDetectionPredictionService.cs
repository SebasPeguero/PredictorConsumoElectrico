using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PredictorConsumoElectrico.Core.DTOs;
using PredictorConsumoElectrico.Core.Interfaces;

namespace PredictorConsumoElectrico.Core.Services
{
    public class TrendDetectionPredictionService : IPredictionService
    {
        public PredictionResultDto Predecir(List<ConsumoRecordDto> datosHistorial)
        {
         decimal aumentos = 0;
         decimal disminuciones = 0;
         decimal estables = 0;

            for (int i = 1; i < 12; i++) 
            {
                if (datosHistorial[i].Consumo > datosHistorial[i - 1].Consumo)
                {
                    aumentos++;
                }
                else if (datosHistorial[i].Consumo < datosHistorial[i - 1].Consumo)
                {
                    disminuciones++;
                }
                else
                {
                    estables++;
                }
            }

            string tendenciaGeneral = "Estable";
            decimal maximo = estables;

            if (aumentos > maximo)
            {
                maximo = aumentos;
                tendenciaGeneral = "Alcista";
            }

            if (disminuciones > maximo)
            {
                tendenciaGeneral = "Bajista";
            }

            return new PredictionResultDto
            {

                ConsumoProximoMes = datosHistorial.Last().Consumo,
               
                Tendencia = tendenciaGeneral,

                NombreMetodo = "Detección de Tendencia",

                MensajeDetallado = $"Aumentos: {aumentos} | Disminuciones: {disminuciones} | Estables: {estables}"
            };

        }
    }
}
