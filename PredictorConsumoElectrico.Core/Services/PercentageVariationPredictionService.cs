using PredictorConsumoElectrico.Core.DTOs;
using PredictorConsumoElectrico.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredictorConsumoElectrico.Core.Services
{
    public class PercentageVariationPredictionService : IPredictionService
    {
        public PredictionResultDto Predecir(List<ConsumoRecordDto> datosHistorial)
        {
            List<decimal> variaciones = new List<decimal>();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Variación mes a mes:");

            // 1. Recorrer desde el mes 2 (índice 1) hasta el final
            for (int i = 1; i < datosHistorial.Count; i++)
            {
                decimal consumoAnterior = datosHistorial[i - 1].Consumo;
                decimal consumoActual = datosHistorial[i].Consumo;

                // 2. Validación de división por cero
                if (consumoAnterior == 0)
                {
                    sb.AppendLine($"Mes {i + 1} -> N/A (Consumo anterior es 0)");
                    continue;
                }

                // 3. Aplicar la fórmula de variación porcentual
                decimal variacionActual = ((consumoActual - consumoAnterior) / consumoAnterior) * 100;

                variaciones.Add(variacionActual);

                // 4. Agregar el resultado al StringBuilder con formato de 2 decimales
                sb.AppendLine($"Mes {i + 1} -> {Math.Round(variacionActual, 2)}%");
            }

            // 5. Calcular el promedio general
            decimal promedioVariacion = variaciones.Any() ? variaciones.Average() : 0;
            sb.AppendLine($"---");
            sb.AppendLine($"Promedio General: {Math.Round(promedioVariacion, 2)}%");

            // 6. Determinar tendencia (Regla: > 1% = Alcista, < -1% = Bajista, el resto = Estable)
            string tendencia = "Estable";
            if (promedioVariacion > 1)
            {
                // El documento usa "aumentando", lo mapeamos a tu estándar "Alcista"
                tendencia = "Alcista";
            }
            else if (promedioVariacion < -1)
            {
                // El documento usa "disminuyendo", lo mapeamos a "Bajista"
                tendencia = "Bajista";
            }

            // 7. Calcular el consumo del próximo mes
            // Tomamos el último registro y le aplicamos el porcentaje de crecimiento/decrecimiento promedio
            decimal ultimoConsumo = datosHistorial.Last().Consumo;
            decimal prediccionMes13 = ultimoConsumo * (1 + (promedioVariacion / 100));

            return new PredictionResultDto
            {
                // Evitamos predicciones negativas en caso de una caída drástica
                ConsumoProximoMes = Math.Round(Math.Max(0, prediccionMes13), 2),
                Tendencia = tendencia,
                NombreMetodo = "Variación Porcentual",
                MensajeDetallado = sb.ToString()
            };
        }
    }
}