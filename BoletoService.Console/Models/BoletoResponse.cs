namespace BoletoService.Console.Models;

public class BoletoResponse
{
    public string CodigoDeBarras { get; private set; }
    public string LinhaDigitavel { get; private set; }
    public BoletoResponse(string codigoDeBarras, string linhaDigitavel)
    {
        CodigoDeBarras = codigoDeBarras;
        LinhaDigitavel = linhaDigitavel;
    }
}
