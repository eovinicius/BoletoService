using BoletoService.Console.Models;

namespace BoletoService.Console.UseCases;

public partial class BoletoCodigoBarrasGenerator
{
    public class ValorTotalDefaultStrategy : IValorTotalStrategy
    {
        public decimal CalcularValorTotal(Boleto boleto)
        {
            decimal valorTotal = boleto.ValorFatura;
            if (boleto.Desconto is not null && boleto.Desconto > 0)
            {
                valorTotal -= boleto.Desconto.Value;
            }
            return valorTotal;
        }
    }
}