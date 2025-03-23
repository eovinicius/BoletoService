using System.Globalization;

namespace BoletoService.Console;

public class BuilderBoleto
{
    private const string MODULO_11 = "8480";
    private const string CODIGO_CONVENIO = "0379";
    private const string CANAL_EMISSOR = "88";

    public Boleto Execute(decimal valorTotal, string codigoCliente, string numeroFatura)
    {
        string valorTotalFormatado = valorTotal.ToString("F2", CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "").PadLeft(11, '0');
        string codigoClienteFormatado = codigoCliente.PadLeft(10, '0');
        string numeroFaturaFormatodo = numeroFatura.PadLeft(13, '0');

        string codigoBarras = string.Concat(
            MODULO_11,
            valorTotalFormatado.PadLeft(11, '0'),
            CODIGO_CONVENIO,
            codigoClienteFormatado,
            numeroFaturaFormatodo,
            CANAL_EMISSOR);

        var result = CalculaDigitosVerificadores(codigoBarras);

        var x = $"{codigoBarras}, {result.CodigoDeBarras}";

        return result;
    }

    private Boleto CalculaDigitosVerificadores(string valor)
    {
        int dvGeral = CalcularModulo11(valor, MultiplicadoresModulo11.Geral);
        valor = string.Concat(valor.Substring(0, 3), dvGeral, valor.Substring(4));

        int dvBloco1 = CalcularModulo11(valor, MultiplicadoresModulo11.Bloco1);
        int dvBloco2 = CalcularModulo11(valor, MultiplicadoresModulo11.Bloco2);
        int dvBloco3 = CalcularModulo11(valor, MultiplicadoresModulo11.Bloco3);
        int dvBloco4 = CalcularModulo11(valor, MultiplicadoresModulo11.Bloco4);

        string linhaDigitavel = FormataLinhaDigitavel(valor, dvBloco1, dvBloco2, dvBloco3, dvBloco4);

        return new Boleto(valor, linhaDigitavel);
    }

    private int CalcularModulo11(string valor, Dictionary<int, int> multiplicadores)
    {
        int soma = 0;
        for (int i = 0; i < valor.Length; i++)
        {
            if (multiplicadores.TryGetValue(i + 1, out int multiplicador))
            {
                soma += (valor[i] - '0') * multiplicador;
            }
        }

        int resto = soma % 11;
        return resto switch
        {
            0 or 1 => 0,
            10 => 1,
            _ => 11 - resto
        };
    }

    private string FormataLinhaDigitavel(string valor, int dv1, int dv2, int dv3, int dv4)
    {
        return string.Format(
            "{0}-{1} {2}-{3} {4}-{5} {6}-{7}",
            valor.Substring(0, 11), dv1,
            valor.Substring(11, 11), dv2,
            valor.Substring(22, 11), dv3,
            valor.Substring(33, 11), dv4
        );
    }
}