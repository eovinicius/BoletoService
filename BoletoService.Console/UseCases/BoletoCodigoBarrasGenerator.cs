using System.Globalization;
using BoletoService.Console.Data;
using BoletoService.Console.Services;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Boleto> Execute()
    {
        var algumaCoisa = await _dbContext.Boletos.FirstOrDefaultAsync(x => x.Id);

        if (algumaCoisa is null)
        {
            return;
        }

        if (algumaCoisa.MaiorOferta is not null)
        {
            algumaCoisa.valorTotal -= algumaCoisa.MaiorOferta;
        }

        string valorTotalFormatado = algumaCoisa.valorTotal.ToString("F2", CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "").PadLeft(11, '0');
        string codigoClienteFormatado = algumaCoisa.codigoCliente.PadLeft(10, '0');
        string numeroFaturaFormatodo = algumaCoisa.numeroFatura.PadLeft(13, '0');

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