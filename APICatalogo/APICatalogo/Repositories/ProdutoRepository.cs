using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Produto> CreateAsync(Produto produto)
    {
        if (produto is null)
            throw new ArgumentNullException("Produto é null");

        await _context.AddAsync(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto is not null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<Produto> GetProdutoAsync(int id)
    {
        var produto = await _context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProdutoId == id);

        if (produto is null)
            throw new ArgumentNullException("Produto não encontrado");

        return produto;
    }

    public async Task<IQueryable<Produto>> GetProdutosAsync()
    {
        var produtos = await _context.Produtos
            .AsNoTracking()
            .ToListAsync();

        if (produtos is null || produtos.Count == 0)
            throw new ArgumentNullException("Produtos não encontrados");

        return (IQueryable<Produto>)produtos;
    }

    public async Task<bool> UpdateAsync(Produto produto)
    {
        if (produto is null)
            throw new ArgumentNullException("Produto é null");

        if (await _context.Produtos.AnyAsync(p => p.ProdutoId == produto.ProdutoId))
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
