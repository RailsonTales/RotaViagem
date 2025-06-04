using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class RotaServiceTests
{
    private RotaContext GetContextComDados()
    {
        var options = new DbContextOptionsBuilder<RotaContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new RotaContext(options);
        context.Rotas.AddRange(
            new Rota { Id = 1, Origem = "A", Destino = "B", Valor = 10 },
            new Rota { Id = 2, Origem = "B", Destino = "C", Valor = 20 },
            new Rota { Id = 3, Origem = "A", Destino = "C", Valor = 50 }
        );
        context.SaveChanges();
        return context;
    }

    [Fact]
    public void GetAll_DeveRetornarTodasAsRotas()
    {
        var context = GetContextComDados();
        var service = new RotaService(context);

        var rotas = service.GetAll();

        Assert.Equal(3, rotas.Count());
    }

    [Fact]
    public void GetById_DeveRetornarRotaCorreta()
    {
        var context = GetContextComDados();
        var service = new RotaService(context);

        var rota = service.GetById(1);

        Assert.NotNull(rota);
        Assert.Equal("A", rota!.Origem);
    }

    [Fact]
    public void Add_DeveAdicionarRota()
    {
        var options = new DbContextOptionsBuilder<RotaContext>()
            .UseInMemoryDatabase(databaseName: "AddRotaTest")
            .Options;

        using var context = new RotaContext(options);
        var service = new RotaService(context);

        service.Add(new Rota { Origem = "X", Destino = "Y", Valor = 99 });

        Assert.Single(context.Rotas);
        Assert.Equal("X", context.Rotas.First().Origem);
    }

    [Fact]
    public void Delete_DeveRemoverRota()
    {
        var context = GetContextComDados();
        var service = new RotaService(context);

        service.Delete(1);

        Assert.Null(context.Rotas.Find(1));
    }

    [Fact]
    public void CalcularMelhorRota_DeveRetornarMenorCaminho()
    {
        var context = GetContextComDados();
        var service = new RotaService(context);

        var (caminho, custo) = service.CalcularMelhorRota("A", "C");

        Assert.Equal(new List<string> { "A", "B", "C" }, caminho);
        Assert.Equal(30, custo);
    }
}
