using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public async Task <ActionResult<IEnumerable<Produto>>> GetAsync()
    {
        var produtos = await _produtoRepository.GetProdutosAsync();
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public async Task<ActionResult<Produto>> GetAsync(int id)
    {
        var produto = await _produtoRepository.GetProdutoAsync(id);
        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Produto produto)
    {
        if (produto is null) return BadRequest(); 
        var novoProduto =  await _produtoRepository.CreateAsync(produto);
        return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutAsync(int id, Produto produto)
    {
        if (id != produto.ProdutoId) return BadRequest();
        var atualizado = await _produtoRepository.UpdateAsync(produto);
        if (atualizado) return Ok(produto);
        else return StatusCode(500, $"Falha ao atualizar o produto de id = {id}");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var produto = await _produtoRepository.GetProdutoAsync(id);
        if (produto is null) return NotFound($"Produto {id} não encontrado.");
        var deletado = await _produtoRepository.DeleteAsync(id);
        if (deletado) return Ok($"Produto de id={id} foi excluído");
        else return StatusCode(500, $"Falha ao exlucir o produto de id={id}");
    }
}
