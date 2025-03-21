using BoletoService.Console;

var valorTotal = 24119.00m;
var codigoCliente = "1511116008";
var numeroFatura = "400479216229";

(string x, string y) = BuilderCodigoBarra.Execute(valorTotal, codigoCliente, numeroFatura);
Console.WriteLine(x);
Console.WriteLine(y);
