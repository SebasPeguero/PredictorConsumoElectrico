# Predictor de Consumo Eléctrico ⚡

Una aplicación web desarrollada en **ASP.NET Core MVC (.NET 10)** diseñada para analizar y predecir el consumo eléctrico futuro basándose en datos históricos de 12 meses. Este proyecto implementa varios algoritmos matemáticos y estadísticos para ofrecer proyecciones precisas y ayudar en la toma de decisiones.

## 🚀 Características Principales

*   **Ingreso de Datos Históricos:** Interfaz para introducir los registros de consumo eléctrico de los últimos 12 meses.
*   **Múltiples Algoritmos de Predicción:** Permite al usuario elegir entre diferentes modelos matemáticos para estimar el consumo del próximo mes.
*   **Análisis de Tendencia:** Determina automáticamente si el consumo tiende a ser Alcista, Bajista o Estable.
*   **Arquitectura Limpia:** Separación clara de responsabilidades entre la lógica de negocio (Core) y la interfaz de usuario (Web).

## 🧠 Métodos de Predicción Implementados

El sistema cuenta con una capa de servicios (`IPredictionService`) que implementa los siguientes algoritmos:

1.  **Promedio Móvil Simple (SMA - Simple Moving Average):**
    Analiza los últimos 3 meses de consumo y calcula un promedio para suavizar fluctuaciones a corto plazo y predecir el mes siguiente.
2.  **Variación Porcentual:**
    Calcula el porcentaje de crecimiento o decrecimiento entre los dos últimos meses registrados y aplica esta misma tasa de variación para proyectar el futuro.
3.  **Detección de Tendencia (Regresión Lineal):**
    Utiliza el método de mínimos cuadrados sobre la serie completa de 12 meses para encontrar la línea de mejor ajuste (pendiente) y proyectar el valor exacto del mes 13, evaluando el comportamiento global del año.

## 🏗️ Arquitectura del Proyecto

El proyecto está dividido en dos capas principales siguiendo principios de Clean Architecture:

*   **`PredictorConsumoElectrico.Core`:** Biblioteca de clases (Class Library) que contiene toda la lógica de negocio, entidades (DTOs) y las implementaciones de los algoritmos de predicción. Es totalmente independiente de la interfaz de usuario.
    *   `/DTOs`: Objetos de transferencia de datos (`ConsumoRecordDto`, `PredictionResultDto`).
    *   `/Interfaces`: Contratos como `IPredictionService`.
    *   `/Services`: Lógica matemática de cada modelo de predicción.
*   **`PredictorConsumoElectrico` (Web):** Proyecto ASP.NET Core MVC que maneja la interacción con el usuario, los controladores y las vistas (Razor Pages). Consume los servicios de la capa Core.

## 🛠️ Tecnologías Utilizadas

*   C# 12 / .NET 10.0
*   ASP.NET Core MVC
*   LINQ (Language Integrated Query)
*   Bootstrap & jQuery (Frontend)

## ⚙️ Instalación y Ejecución

### Prerrequisitos
*   [Visual Studio 2022](https://visualstudio.microsoft.com/) o superior (con la carga de trabajo de desarrollo web y ASP.NET).
*   [.NET 10.0 SDK](https://dotnet.microsoft.com/download) instalado.

### Pasos para ejecutar localmente

1.  Clona este repositorio:
    ```bash
    git clone https://github.com/tu-usuario/PredictorConsumoElectrico.git
    ```
2.  Abre la solución `PredictorConsumoElectrico.slnx` (o el archivo `.csproj` principal) en Visual Studio.
3.  Asegúrate de que el proyecto `PredictorConsumoElectrico` (el proyecto MVC, no el Core) esté seleccionado como **Proyecto de inicio**.
4.  Restaura los paquetes NuGet si es necesario (generalmente Visual Studio lo hace automáticamente).
5.  Presiona `F5` o haz clic en "Iniciar depuración" para compilar y ejecutar la aplicación en tu navegador web.

## 📝 Notas de Desarrollo

Se ha utilizado el modificador `required` en los DTOs aprovechando las características modernas de C# junto con `Nullable Reference Types` para garantizar la integridad de los datos entre la capa de presentación y la capa de lógica de negocio.

---
*Desarrollado para la asignatura de Programación III.*