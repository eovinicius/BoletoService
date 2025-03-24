using Moq;
using Bogus;
using BoletoService.Console.Models;
using BoletoService.Console.Repositories;
using BoletoService.Console.UseCases;
using static BoletoService.Console.UseCases.BoletoCodigoBarrasGenerator;

namespace BoletoService.Test;

public class BoletoCodigoBarrasGeneratorTest
{
    [Fact]
    public async Task DeveRetornarBoletoResponseQuandoBoletoExistir()
    {
        // Arrange
        var faker = new Faker();
        var boletoMock = new Boleto()
        {
            CodigoCliente = faker.Random.Number(100000, 999999).ToString(),
            NumeroFatura = faker.Random.Number(1000000000, 999999999).ToString(),
            ValorFatura = faker.Random.Decimal(10, 1000)
        };

        var repositoryMock = new Mock<IBoletoRepository>();
        repositoryMock.Setup(r => r.GetFirst()).ReturnsAsync(boletoMock);

        var valorTotalStrategyMock = new Mock<IValorTotalStrategy>();
        valorTotalStrategyMock.Setup(v => v.CalcularValorTotal(boletoMock)).Returns(boletoMock.ValorFatura);

        var generator = new BoletoCodigoBarrasGenerator(repositoryMock.Object, valorTotalStrategyMock.Object);

        // Act
        var result = await generator.Execute();

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result.CodigoDeBarras));
    }
}
