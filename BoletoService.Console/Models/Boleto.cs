namespace BoletoService.Console.Models;

public class Boleto
{
    public Guid Id { get; private set; }
    public string CodigoCliente { get; private set; }
    public string NumeroFatura { get; private set; }
    public decimal ValorFatura { get; private set; }
    public decimal? Desconto { get; private set; }
    public DateOnly DataArquivo { get; private set; }
}
