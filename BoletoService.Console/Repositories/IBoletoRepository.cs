using BoletoService.Console.Models;

namespace BoletoService.Console.Repositories;

public interface IBoletoRepository
{
    Task<Boleto?> GetFirst();
}
