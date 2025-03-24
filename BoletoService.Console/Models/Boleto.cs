namespace BoletoService.Console.Models;

public class Boleto
{
    public Guid Id { get; private set; }
    public string CodigoCliente { get; set; }
    public string NumeroFatura { get; set; }
    public decimal ValorFatura { get; set; }
    public decimal? Desconto { get; set; }
    public DateOnly DataArquivo { get; set; }
}
