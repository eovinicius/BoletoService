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
        var valorTotal = 16830.00m;
        var codigoCliente = "1504788553";
        var numeroFatura = "400479132899";

        // Act
        var builderBoleto = new BuilderBoleto();
        var boleto = builderBoleto.Execute(valorTotal, codigoCliente, numeroFatura);

        // Assert
        Assert.NotNull(boleto.CodigoDeBarras);
        Assert.NotNull(boleto.LinhaDigitavel);
        Assert.Equal("84860000124600003791512471230040047913289988", boleto.CodigoDeBarras);
        Assert.Equal("84860000124-0 60000379151-1 24712300400-3 47913289988-4", boleto.LinhaDigitavel);
    }

    [Fact]
    public void CalculaDigitosVerificadoresDeveRetornarValoresCorretos3()
    {
        // Arrange
        var valorTotal = 1622.38m;
        var codigoCliente = "1537033410";
        var numeroFatura = "402010347895";

        // Act
        var builderBoleto = new BuilderBoleto();
        var boleto = builderBoleto.Execute(valorTotal, codigoCliente, numeroFatura);

        // Assert
        Assert.NotNull(boleto.CodigoDeBarras);
        Assert.NotNull(boleto.LinhaDigitavel);
        Assert.Equal("84800000016223803791537033410040201034789596", boleto.CodigoDeBarras);
        Assert.Equal("84800000016-2 22380379153-6 70334100402-1 01034789596-1", boleto.LinhaDigitavel);
    }
}