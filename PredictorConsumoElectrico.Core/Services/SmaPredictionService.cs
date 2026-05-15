using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PredictorConsumoElectrico.Core.DTOs;
using PredictorConsumoElectrico.Core.Interfaces;

namespace PredictorConsumoElectrico.Core.Services
{
    public class SmaPredictionService : IPredictionService
    {
        public PredictionResultDto Predecir(List<ConsumoRecordDto> datosHistorial)
        {
            // 1. Aislamiento de los últimos 3 registros (ordenados de antiguo a reciente)
            var ultimosTresConsumos = datosHistorial
                .Skip(Math.Max(0, datosHistorial.Count - 3))
                .Select(d => (decimal)d.Consumo)
                .ToList();

            // 2. Cálculo del promedio móvil simple (SMA)
            decimal promedio = ultimosTresConsumos.Average();

            // 3. Obtención del último consumo para análisis de tendencia
            decimal ultimoConsumo = (decimal)datosHistorial.Last().Consumo;

            // 4. Lógica de determinación de tendencia
            string tendencia;
            if (promedio > ultimoConsumo)
            {
                tendencia = "Alcista";
            }
            else if (promedio < ultimoConsumo)
            {
                tendencia = "Bajista";
            }
            else
            {
                tendencia = "Estable";
            }

            // 5. Retorno del DTO poblado
            
            return new PredictionResultDto
            {
                ConsumoProximoMes = Math.Round(promedio, 2),
                Tendencia = tendencia,
                NombreMetodo = "Promedio Móvil Simple (SMA)",
                MensajeDetallado = ""
            };
        }
    }
}
