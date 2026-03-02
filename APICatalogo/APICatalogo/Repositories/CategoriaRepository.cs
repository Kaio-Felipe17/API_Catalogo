using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParams)
    {
        var categorias = GetAll()
            .OrderBy(c => c.CategoriaId)
            .AsQueryable();

        return PagedList<Categoria>.ToPagedList(
            categorias,
            categoriasParams.PageNumber,
            categoriasParams.PageSize);
    }

    public PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasParams)
    {
        var categorias = GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(categoriasParams.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriasParams.Nome));
        }

        return PagedList<Categoria>.ToPagedList(
            categorias,
            categoriasParams.PageNumber,
            categoriasParams.PageSize);
    }
}
