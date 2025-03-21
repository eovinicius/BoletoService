using BoletoService.Console;

var valorTotal = 24119.00m;
var codigoCliente = "1511116008";
var numeroFatura = "400479216229";

var boleto = BuilderBoleto.Execute(valorTotal, codigoCliente, numeroFatura);

Console.WriteLine(boleto.CodigoDeBarras);
Console.WriteLine(boleto.LinhaDigitavel);
