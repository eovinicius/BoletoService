using BoletoService.Console.Data;
using BoletoService.Console.UseCases;
using Microsoft.EntityFrameworkCore;
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
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Hour)
    .CreateLogger();

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IConfiguration>(configuration);
serviceCollection.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

serviceCollection.AddTransient<BoletoCodigoBarrasGenerator>();

serviceCollection.AddDbContext<AppDbContext>(options =>
{
    var connectionString = configuration.GetConnectionString("database") ??
                           throw new ArgumentNullException(nameof(configuration));

    options.UseSqlServer("connetionString");
});


var serviceProvider = serviceCollection.BuildServiceProvider();

try
{
    Log.Information("Aplicação iniciada.");
    Log.Information("Ambiente carregado: {Environment}", environment);

    // await serviceProvider.GetRequiredService<BoletoCodigoBarrasGenerator>()!.Execute();
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