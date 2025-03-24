namespace BoletoService.Console;

public class Boleto
{
    public string CodigoDeBarras { get; private set; }
    public string LinhaDigitavel { get; private set; }
    public Boleto(string codigoDeBarras, string linhaDigitavel)
    {
        CodigoDeBarras = codigoDeBarras;
        LinhaDigitavel = linhaDigitavel;
    }
}
