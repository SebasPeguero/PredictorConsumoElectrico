using PredictorConsumoElectrico.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Traducción total del Model Binder al español
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((valor, campo) =>
        $"El valor '{valor}' no es válido para {campo}. Revise la fecha ingresada.");

    options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((campo) =>
        $"El valor proporcionado es inválido para {campo}.");

    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((valor) =>
        $"El valor '{valor}' es inválido.");

    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((campo) =>
        $"El campo {campo} debe ser un número válido.");

    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((campo) =>
        $"El campo {campo} no puede quedar vacío.");

    options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((campo) =>
        $"Falta ingresar un valor para {campo}.");
});
builder.Services.AddSingleton<PredictorConsumoElectrico.Infrastructure.PredictionSettings>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
