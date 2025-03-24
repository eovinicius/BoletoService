using BoletoService.Console.Models;

namespace BoletoService.Console.Services;

public partial class BoletoCodigoBarrasGenerator
{
    public interface IValorTotalStrategy
    {
        decimal CalcularValorTotal(Boleto boleto);
    }
}