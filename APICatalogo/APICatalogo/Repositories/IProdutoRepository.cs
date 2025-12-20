using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface IProdutoRepository
{
    Task<IQueryable<Produto>> GetProdutosAsync();
    Task<Produto> GetProdutoAsync(int id);
    Task<Produto> CreateAsync(Produto produto);
    Task<bool> UpdateAsync(Produto produto);
    Task<bool> DeleteAsync(int id);
}
