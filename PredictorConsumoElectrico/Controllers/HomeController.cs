using Microsoft.AspNetCore.Mvc;
using PredictorConsumoElectrico.Core.DTOs;
using PredictorConsumoElectrico.Core.Interfaces;
using PredictorConsumoElectrico.Core.Services;
using PredictorConsumoElectrico.Infrastructure;
using PredictorConsumoElectrico.Core.ViewModels;
using System.Diagnostics;

namespace PredictorConsumoElectrico.Controllers
{
    public class HomeController : Controller
    {
        private readonly PredictionSettings _settings;

        // Inyectamos el Singleton que mantiene el modo seleccionado en memoria
        public HomeController(PredictionSettings settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Enviamos el ViewModel inicial que ya trae los 12 espacios vacíos listos
            var model = new HomeViewModel();

            if (_settings.HistorialGuardado != null && _settings.HistorialGuardado.Count == 12)
            {
                for (int i = 0; i < 12; i++)
                {
                    model.Registros[i].Fecha = _settings.HistorialGuardado[i].Fecha;
                    model.Registros[i].Valor = _settings.HistorialGuardado[i].Consumo;
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(HomeViewModel model)
        {
            // Validar que existan exactamente 12 registros
            if (model.Registros == null || model.Registros.Count != 12)
            {
                ModelState.AddModelError(string.Empty, "El sistema debe validar que existan exactamente 12 registros para calcular la predicción.");
            }

            // Verificar validaciones de DataAnnotations (No negativos, campos requeridos) 
            if (!ModelState.IsValid)
            {
                model.HayResultado = false;
                return View(model);
            }

            try
            {
                //  Mapeo Convertir de ViewModel (Capa Web) a DTO (Capa Core) 
                var historialDto = model.Registros.Select(r => new ConsumoRecordDto
                {
                    Fecha = r.Fecha.Value,
                    Consumo = r.Valor.Value
                }).ToList();

                _settings.HistorialGuardado = historialDto;

                // Selección del Servicio según el modo guardado en el Singleton 
                IPredictionService predictionService = _settings.ModoSeleccionado switch
                {
                    "RegresionLineal" => new LinearRegressionPredictionService(),
                    "VariacionPorcentual" => new PercentageVariationPredictionService(),
                    "Tendencia" => new TrendDetectionPredictionService(),
                     _ => new SmaPredictionService() // Por defecto SMA [cite: 42, 48]
                };

                // Ejecutar cálculo en la capa de lógica de negocio 
                PredictionResultDto resultado = predictionService.Predecir(historialDto);

                //Mapeo salida Pasar los resultados del DTO al ViewModel para la vista
                model.ConsumoProximoMes = resultado.ConsumoProximoMes;
                model.tendencia = resultado.Tendencia;
                model.NombreMetodoSeleccionado = resultado.NombreMetodo;
                model.MensajeDetallado = resultado.MensajeDetallado;
                model.HayResultado = true;
            }
            catch (Exception ex)
            {
                // Manejo de errores 
                ModelState.AddModelError(string.Empty, $"Error al calcular la predicción: {ex.Message}");
                model.HayResultado = false;
            }

            return View(model);
        }

        
        [HttpGet]
        public IActionResult Modos()
        {
            
            return View(_settings);
        }

       
        [HttpPost]
        public IActionResult Modos(string ModoSeleccionado)
        {
            if (!string.IsNullOrEmpty(ModoSeleccionado))
            {
                // Actualizamos el Singleton en memoria
                _settings.ModoSeleccionado = ModoSeleccionado;

                // Usamos TempData para mostrar un mensaie de éxito 
                TempData["MensajeExito"] = "Modo de predicción actualizado exitosamente.";
            }

            // Recargamos la página para que se vean los cambios
            return RedirectToAction("Modos");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}