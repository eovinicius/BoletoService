using Moq;
using Bogus;
using BoletoService.Console.Models;
using BoletoService.Console.Repositories;
using BoletoService.Console.Services;

namespace BoletoService.Test;

public class BoletoCodigoBarrasGeneratorTest
{
    private readonly Faker faker = new Faker();

    // [Fact]
    // public async Task DeveRetornarBoletoResponseQuandoBoletoExistir()
    // {
    //     // Arrange
    //     var boletoMock = new Boleto()
    //     {
    //         CodigoCliente = faker.Random.Number(100000, 999999).ToString(),
    //         NumeroFatura = faker.Random.Number(1000000000, 999999999).ToString(),
    //         ValorFatura = faker.Random.Decimal(10, 1000)
    //     };

    //     var boletoMock2 = new Boleto()
    //     {
    //         CodigoCliente = "1504788553",
    //         NumeroFatura = "400479132899",
    //         ValorFatura = 16830.00m
    //     };

    //     var repositoryMock = new Mock<IBoletoRepository>();
    //     repositoryMock.Setup(r => r.GetFirst()).ReturnsAsync(boletoMock2);


    //     var generator = new BoletoCodigoBarrasGenerator(repositoryMock.Object);

    //     // Act
    //     var result = await generator.Execute();

    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.False(string.IsNullOrWhiteSpace(result.CodigoDeBarras));
    //     Assert.Equal("84860000124600003791512471230040047913289988", result.CodigoDeBarras);
    //     Assert.Equal("84860000124-0 60000379151-1 24712300400-3 47913289988-4", result.LinhaDigitavel);
    // }

    [Fact]
    public void DeveCalcularValorComDescontoCorretamente()
    {
        // Arrange
        var valorFatura = faker.Random.Decimal(1000, 100000);
        var desconto = faker.Random.Bool() ? faker.Random.Decimal(10, 100) : (decimal?)null;

        var boleto = new Boleto()
        {
            CodigoCliente = faker.Random.Number(100000, 999999).ToString(),
            NumeroFatura = faker.Random.Number(1000000000, 999999999).ToString(),
            ValorFatura = valorFatura,
            Desconto = desconto
        };

        var valorEsperado = desconto.HasValue ? valorFatura - desconto.Value : valorFatura;

        // Act
        var valorComDesconto = CalculaDescontos.Calcular(boleto.ValorFatura, boleto.Desconto);

        // Assert
        Assert.Equal(valorEsperado, valorComDesconto);
    }
}
