using PredictorConsumoElectrico.Core.DTOs;
using PredictorConsumoElectrico.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredictorConsumoElectrico.Core.Services
{
    public class LinearRegressionPredictionService : IPredictionService
    {
        public PredictionResultDto Predecir(List<ConsumoRecordDto> datosHistorial)
        {
            // 1. Definición de variables base
            int n = datosHistorial.Count; 
            decimal sumX = 0;
            decimal sumY = 0;
            decimal sumXY = 0;
            decimal sumX2 = 0;

            // 2. Calcular las sumatorias iterando sobre los datos
            for (int i = 0; i < n; i++)
            {
                // En regresión temporal, 'x' es el periodo de tiempo (mes 1, mes 2... mes 12)
                decimal x = i + 1;
                decimal y = (decimal)datosHistorial[i].Consumo;

                sumX += x;
                sumY += y;
                sumXY += (x * y);
                sumX2 += (x * x);
            }

            // 3. Calcular la Pendiente (m)
            // m = (Σxy - ((Σx * Σy) / n)) / (Σx² - ((Σx)² / n))
            decimal numeradorM = sumXY - ((sumX * sumY) / n);
            decimal denominadorM = sumX2 - ((sumX * sumX) / n);
            decimal m = numeradorM / denominadorM;

            // 4. Calcular el Intercepto (b)
            // b = ȳ - m * x̄  (donde ȳ y x̄ son los promedios)
            decimal promedioX = sumX / n;
            decimal promedioY = sumY / n;
            decimal b = promedioY - (m * promedioX);

            // 5. Realizar la Predicción para el mes 13 (y = mx + b)
            decimal prediccionMes13 = (m * 13) + b;

            // 6. Evaluar la Tendencia basada exclusivamente en la pendiente (m)
            string tendencia = "Estable";
            if (m > 0)
            {
                tendencia = "Alcista";
            }
            else if (m < 0)
            {
                tendencia = "Bajista";
            }

            // 7. Retornar el DTO
            return new PredictionResultDto
            {
                ConsumoProximoMes = prediccionMes13,
                Tendencia = tendencia,
                NombreMetodo = "Regresión Lineal Simple",
                MensajeDetallado = $"Valor de la pendiente (m): {m:F4}" // :F4 formatea a 4 decimales
            };
        }
    }
}
