using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetCategoriasAsync();
    Task<Categoria> GetCategoriaAsync(int id);
    Task<Categoria> CreateAsync(Categoria categoria);
    Task<Categoria> UpdateAsync(Categoria categoria);
    Task<Categoria> DeleteAsync(int id);
}
