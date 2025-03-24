using System.Globalization;
using BoletoService.Console.Data;
using BoletoService.Console.Services;

namespace BoletoService.Console.UseCases;

public class BoletoCodigoBarrasGenerator
{
    private const string MODULO_11 = "8480";
    private const string CODIGO_CONVENIO = "0379";
    private const string CANAL_EMISSOR = "88";
    private readonly AppDbContext _dbContext;

    public BoletoCodigoBarrasGenerator(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Boleto Execute()
    {
        string valorTotalFormatado = valorTotal.ToString("F2", CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "").PadLeft(11, '0');
        string codigoClienteFormatado = codigoCliente.PadLeft(10, '0');
        string numeroFaturaFormatodo = numeroFatura.PadLeft(13, '0');

        string codigoBarras = string.Concat(
            MODULO_11,
            valorTotalFormatado,
            CODIGO_CONVENIO,
            codigoClienteFormatado,
            numeroFaturaFormatodo,
            CANAL_EMISSOR);

        var result = CalculaDigitosVerificadores.Calcular(codigoBarras);

        var x = $"{codigoBarras}, {result.CodigoDeBarras}";

        return result;
    }


}