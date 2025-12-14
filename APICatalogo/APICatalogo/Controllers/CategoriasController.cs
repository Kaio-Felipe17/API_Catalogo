using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutosAsync()
    {
        return await _context.Categorias
            .Include(p => p.Produtos)
            .Where(c => c.CategoriaId <= 5)
            .AsNoTracking()
            .ToListAsync();
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
    {
        var categorias = await _context.Categorias
            .AsNoTracking()
            .ToListAsync();

        if (categorias is null) return NotFound("Categorias não encontradas.");
        return categorias;
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> GetAsync(int id)
    {
        var categoria = await _context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CategoriaId == id);

        if (categoria is null) return NotFound("Categoria não encontrada.");
        return categoria;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Categoria categoria)
    {
        if (categoria is null) return BadRequest();
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutAsync(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId) return BadRequest();
        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
        if (categoria is null) return NotFound($"Categoria {id} não encontrada.");
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return Ok(categoria);
    }
}
