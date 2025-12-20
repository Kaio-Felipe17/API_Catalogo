using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria> CreateAsync(Categoria categoria)
    {
        if (categoria is null)
            throw new ArgumentNullException("Categoria é null");
        await _context.AddAsync(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> DeleteAsync(int id)
    {
        var categoria = await _context.Categorias
            .FindAsync(id);

        if (categoria is null)
            throw new ArgumentNullException(nameof(categoria));

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> GetCategoriaAsync(int id)
    {
        var categoria = await _context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CategoriaId == id);

        if (categoria is null)
            throw new ArgumentNullException("Categoria não encontrada");

        return categoria;
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
    {
        var categorias = await _context.Categorias
            .AsNoTracking()
            .ToListAsync();

        if (categorias is null || categorias.Count == 0)
            throw new ArgumentNullException("Categorias não encontradas");

        return categorias;
    }

    public async Task<Categoria> UpdateAsync(Categoria categoria)
    {
        if (categoria is null)
            throw new ArgumentNullException(nameof(categoria));

        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return categoria;
    }
}
