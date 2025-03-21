using BoletoService.Console;

namespace BoletoService.Test;

public class BuilderBoletoTests
{
    [Fact]
    public void CalculaDigitosVerificadoresDeveRetornarValoresCorretos()
    {
        // Arrange
        var valorTotal = 24119.00m;
        var codigoCliente = "1511116008";
        var numeroFatura = "400479216229";

        // Act
        var builderBoleto = new BuilderBoleto();
        var boleto = builderBoleto.Execute(valorTotal, codigoCliente, numeroFatura);

        // Assert
        Assert.NotNull(boleto.CodigoDeBarras);
        Assert.NotNull(boleto.LinhaDigitavel);
        Assert.Equal("84800000241190003791511116008040047921622988", boleto.CodigoDeBarras);
        Assert.Equal("84800000241-6 19000379151-5 11160080400-5 47921622988-0", boleto.LinhaDigitavel);
    }

    [Fact]
    public void CalculaDigitosVerificadoresDeveRetornarValoresCorretos2()
    {
        // Arrange
        var valorTotal = 55.93m;
        var codigoCliente = "1538519032";
        var numeroFatura = "402191846138";

        // Act
        var builderBoleto = new BuilderBoleto();
        var boleto = builderBoleto.Execute(valorTotal, codigoCliente, numeroFatura);

        // Assert
        Assert.NotNull(boleto.CodigoDeBarras);
        Assert.NotNull(boleto.LinhaDigitavel);
        Assert.Equal("84850000000559303791538519032040219184613896", boleto.CodigoDeBarras);
        Assert.Equal("84850000000-5 55930379153-7 85190320402-4 19184613896-4", boleto.LinhaDigitavel);
    }
}