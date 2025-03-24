using BoletoService.Console.Models;

namespace BoletoService.Console.Services;

public class CalculaDescontos
{
    public static decimal Calcular(Boleto boleto)
    {
        decimal valorTotal = boleto.ValorFatura;
        if (boleto.Desconto is not null && boleto.Desconto > 0)
        {
            valorTotal -= boleto.Desconto.Value;
        }
        return valorTotal;
    }
}