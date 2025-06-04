//using Xunit;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;

//public class RotaServiceBDTests
//{
//    private RotaService CriarService()
//    {
//        var options = new DbContextOptionsBuilder<RotaContext>()
//            .UseSqlServer("Server=Railson-PC;Database=RotaViagem;Trusted_Connection=Yes;MultipleActiveResultSets=true;TrustServerCertificate=True;")
//            .Options;

//        var context = new RotaContext(options);
//        return new RotaService(context);
//    }

//    [Fact]
//    public void GetAll_DeveRetornarTodasAsRotas()
//    {
//        var service = CriarService();

//        var rotas = service.GetAll();

//        Assert.Equal(7, rotas.Count());
//    }

//    [Fact]
//    public void GetById_DeveRetornarRotaCorreta()
//    {
//        var service = CriarService();

//        var rota = service.GetById(1);

//        Assert.NotNull(rota);
//        Assert.Equal("GRU", rota!.Origem);
//    }

//    [Fact]
//    public void CalcularMelhorRota_DeveRetornarGRUParaCDGMaisBarata()
//    {
//        var service = CriarService();

//        var (caminho, custo) = service.CalcularMelhorRota("GRU", "CDG");

//        Assert.Equal(new List<string> { "GRU", "ORL", "CDG" }, caminho);
//        Assert.Equal(61.00m, custo);
//    }
//}
