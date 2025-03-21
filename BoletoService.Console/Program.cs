using BoletoService.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var serviceCollection = new ServiceCollection();
try
{
    Log.Information("Aplicação iniciada.");
    Log.Information("Ambiente carregado: {Environment}", environment);

    var serviceProvider = serviceCollection.BuildServiceProvider();

    var builderBoleto = serviceProvider.GetService<BuilderBoleto>()!;
    // Arrange
    var valorTotal = 55.93m;
    var codigoCliente = "1538519032";
    var numeroFatura = "402191846138";

    // Act
    var builderBolet = new BuilderBoleto();
    var boleto = builderBolet.Execute(valorTotal, codigoCliente, numeroFatura);

    var x = builderBoleto.Execute(valorTotal, codigoCliente, numeroFatura);
}
catch (Exception ex)
{
    Log.Error(ex, "Ocorreu um erro inesperado.");
}
finally
{
    Log.Information("Aplicação finalizada.");
    Log.CloseAndFlush();
}