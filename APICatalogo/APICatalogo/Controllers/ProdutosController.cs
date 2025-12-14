using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task <ActionResult<IEnumerable<Produto>>> GetAsync()
    {
        var produtos = await _context.Produtos
            .AsNoTracking()
            .Take(10)
            .ToListAsync();

        if (produtos is null) return NotFound("Produtos não encontrados.");
        return produtos;

    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public async Task<ActionResult<Produto>> GetAsync(int id)
    {
        var produto = await _context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProdutoId == id);

        if (produto is null) return NotFound("Produto não encontrado.");
        return produto;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Produto produto)
    {
        if (produto is null) return BadRequest(); 
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();
        return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutAsync(int id, Produto produto)
    {
        if (id != produto.ProdutoId) return BadRequest();
        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
        if (produto is null) return NotFound($"Produto {id} não encontrado.");
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return Ok(produto);
    }
}
