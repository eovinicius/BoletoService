using System.Globalization;
using BoletoService.Console.Models;
using BoletoService.Console.Repositories;
using BoletoService.Console.Services;

namespace BoletoService.Console.UseCases;

public partial class BoletoCodigoBarrasGenerator
{
    private const string MODULO_11 = "8480";
    private const string CODIGO_CONVENIO = "0379";
    private const string CANAL_EMISSOR = "88";
    private readonly IBoletoRepository _repository;

    public BoletoCodigoBarrasGenerator(IBoletoRepository repository)
    {
        _repository = repository;
    }

    public async Task<BoletoResponse> Execute()
    {
        var Valor = await _repository.GetFirst();

        if (Valor is null)
        {
            throw new InvalidOperationException("Boleto não encontrado.");
        }

        var valorTotal = CalculaDescontos.Calcular(Valor);

        string valorTotalFormatado = valorTotal.ToString("F2", CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "").PadLeft(11, '0');
        string codigoClienteFormatado = Valor.CodigoCliente.PadLeft(10, '0');
        string numeroFaturaFormatodo = Valor.NumeroFatura.PadLeft(13, '0');

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