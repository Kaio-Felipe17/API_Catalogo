using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;
    // Only IProdutoRepository injection would be necessary here, since it inherits IRepository
    private readonly IRepository<Produto> _repository;

    public ProdutosController(
        IProdutoRepository produtoRepository,
        IRepository<Produto> repository)
    {
        _produtoRepository = produtoRepository;
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
       var produtos = _produtoRepository.GetProdutosPorCategoria(id);
       if (produtos is null) return NotFound();
       return Ok(produtos);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _repository.GetAll();
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.Get(p => p.ProdutoId == id);
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null) return BadRequest(); 
        var novoProduto = _repository.Create(produto);
        return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId) return BadRequest();
        var atualizado = _repository.Update(produto);
        return Ok(atualizado);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _repository.Get(p => p.ProdutoId == id);
        if (produto is null) return NotFound($"Produto {id} não encontrado.");
        var deletado = _repository.Delete(produto);
        return Ok(deletado);
    }
}
