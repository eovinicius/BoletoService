using BoletoService.Console.Models;

namespace BoletoService.Console.UseCases;

public partial class BoletoCodigoBarrasGenerator
{
    public interface IValorTotalStrategy
    {
        decimal CalcularValorTotal(Boleto boleto);
    }
}