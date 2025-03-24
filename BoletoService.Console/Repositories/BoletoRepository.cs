using BoletoService.Console.Data;
using BoletoService.Console.Models;
using Microsoft.EntityFrameworkCore;

namespace BoletoService.Console.Repositories;

public class BoletoRepository : IBoletoRepository
{

    private readonly AppDbContext _dbContext;

    public BoletoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Boleto?> GetFirst()
    {
        return await _dbContext.Boletos.FirstOrDefaultAsync();
    }
}
