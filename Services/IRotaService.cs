
public interface IRotaService
{
    IEnumerable<Rota> GetAll();
    Rota? GetById(int id);
    void Add(Rota rota);
    void Update(Rota rota);
    void Delete(int id);
    (List<string> caminho, decimal custo) CalcularMelhorRota(string origem, string destino);
}
