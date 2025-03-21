using BoletoService.Console;

namespace BoletoService.Test;

public class BuilderBoletoTests
{
    [Fact]
    public void CalculaDigitosVerificadores_DeveRetornarValoresCorretos()
    {
        // Arrange
        var valorTotal = 24119.00m;
        var codigoCliente = "1511116008";
        var numeroFatura = "400479216229";

        // Act
        var boleto = BuilderBoleto.Execute(valorTotal, codigoCliente, numeroFatura);

        // Assert
        Assert.NotNull(boleto.CodigoDeBarras);
        Assert.NotNull(boleto.LinhaDigitavel);
        Assert.Equal("84800000241190003791511116008040047921622988", boleto.CodigoDeBarras);
        Assert.Equal("84800000241-6 19000379151-5 11160080400-5 47921622988-0", boleto.LinhaDigitavel);
    }
}