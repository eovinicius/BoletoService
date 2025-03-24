namespace BoletoService.Console.Services;

public class CalculaDigitosVerificadores
{
    public static Boleto Calcular(string valor)
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

    private static int CalcularModulo11(string valor, Dictionary<int, int> multiplicadores)
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

    private static string FormataLinhaDigitavel(string valor, int dv1, int dv2, int dv3, int dv4)
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
