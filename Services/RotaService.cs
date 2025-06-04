
using Microsoft.EntityFrameworkCore;

public class RotaService : IRotaService
{
    private readonly RotaContext _context;

    public RotaService(RotaContext context)
    {
        _context = context;
    }

    public IEnumerable<Rota> GetAll() => _context.Rotas.ToList();

    public Rota? GetById(int id) => _context.Rotas.Find(id);

    public void Add(Rota rota)
    {
        _context.Rotas.Add(rota);
        _context.SaveChanges();
    }

    public void Update(Rota rota)
    {
        _context.Rotas.Update(rota);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var rota = _context.Rotas.Find(id);
        if (rota != null)
        {
            _context.Rotas.Remove(rota);
            _context.SaveChanges();
        }
    }

    public (List<string> caminho, decimal custo) CalcularMelhorRota(string origem, string destino)
    {
        var grafo = _context.Rotas.AsEnumerable()
            .GroupBy(r => r.Origem)
            .ToDictionary(g => g.Key, g => g.Select(r => (r.Destino, r.Valor)).ToList());

        var dist = new Dictionary<string, decimal>();
        var prev = new Dictionary<string, string?>();
        var visited = new HashSet<string>();
        var queue = new PriorityQueue<string, decimal>();

        foreach (var node in grafo.Keys)
        {
            dist[node] = decimal.MaxValue;
            prev[node] = null;
        }

        dist[origem] = 0;
        queue.Enqueue(origem, 0);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (visited.Contains(current)) continue;
            visited.Add(current);

            if (!grafo.ContainsKey(current)) continue;

            foreach (var (vizinho, custo) in grafo[current])
            {
                var alt = dist[current] + custo;
                if (alt < dist.GetValueOrDefault(vizinho, decimal.MaxValue))
                {
                    dist[vizinho] = alt;
                    prev[vizinho] = current;
                    queue.Enqueue(vizinho, alt);
                }
            }
        }

        if (!dist.ContainsKey(destino) || dist[destino] == decimal.MaxValue)
            return (new List<string>(), 0);

        var caminho = new List<string>();
        var atual = destino;
        while (atual != null)
        {
            caminho.Insert(0, atual);
            atual = prev[atual];
        }

        return (caminho, dist[destino]);
    }
}
