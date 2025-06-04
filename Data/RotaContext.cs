
using Microsoft.EntityFrameworkCore;

public class RotaContext : DbContext
{
    public RotaContext(DbContextOptions<RotaContext> options) : base(options) { }

    public DbSet<Rota> Rotas { get; set; }
}
