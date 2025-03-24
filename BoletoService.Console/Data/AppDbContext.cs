using BoletoService.Console.Models;
using Microsoft.EntityFrameworkCore;

namespace BoletoService.Console.Data;

public class AppDbContext : DbContext
{
    public DbSet<Boleto> Boletos { get; set; }
    protected AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
