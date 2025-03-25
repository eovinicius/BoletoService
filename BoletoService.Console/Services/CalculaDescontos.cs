using BoletoService.Console.Models;

namespace BoletoService.Console.Services;

public class CalculaDescontos
{
    public static decimal Calcular(Decimal valorFatura, decimal? desconto)
    {
        decimal valorTotal = valorFatura;
        if (desconto is not null && desconto > 0)
        {
            valorTotal -= desconto.Value;
        }
        return valorTotal;
    }
}