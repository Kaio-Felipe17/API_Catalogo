using APICatalogo.Context;

namespace APICatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository? _produtoRepo;
    private ICategoriaRepository? _categoriaRepo;
    public AppDbContext context;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(context);
        }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(context);
        }
    }

    public async Task CommitAsync() => await context.SaveChangesAsync();

    public void Dispose() => context.Dispose();
}
