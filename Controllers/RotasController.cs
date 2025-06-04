
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RotasController : ControllerBase
{
    private readonly IRotaService _service;

    public RotasController(IRotaService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var rota = _service.GetById(id);
        return rota == null ? NotFound() : Ok(rota);
    }

    [HttpPost]
    public IActionResult Add(Rota rota)
    {
        _service.Add(rota);
        return CreatedAtAction(nameof(GetById), new { id = rota.Id }, rota);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Rota rota)
    {
        if (id != rota.Id) return BadRequest();
        _service.Update(rota);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }

    [HttpGet("consulta")]
    public IActionResult Consulta([FromQuery] string origem, [FromQuery] string destino)
    {
        var (caminho, custo) = _service.CalcularMelhorRota(origem, destino);
        if (!caminho.Any()) return NotFound("Rota não encontrada");
        return Ok(new { Caminho = string.Join(" - ", caminho), Custo = custo });
    }
}
