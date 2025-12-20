using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _repository;

    public CategoriasController(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
    {
        var categorias = await _repository.GetCategoriasAsync();
        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> GetAsync(int id)
    {
        var categoria = await _repository.GetCategoriaAsync(id);
        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Categoria categoria)
    {
        if (categoria is null) return BadRequest();
        var categoriaCriada = await _repository.CreateAsync(categoria);
        return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutAsync(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId) return BadRequest();
        await _repository.UpdateAsync(categoria);
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var categoria = await _repository.GetCategoriaAsync(id);
        await _repository.DeleteAsync(id);
        return Ok(categoria);
    }
}
