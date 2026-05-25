using Microsoft.EntityFrameworkCore;
using PROJETO.MEU.Data;
using PROJETO.MEU.Models;

namespace PROJETO.MEU.Repositories;

public interface IRefeicaoRepository : IRepository<Refeicao>
{
    Task<Refeicao?> GetByNameAsync(string nome);
    Task<IEnumerable<Refeicao>> GetByTipoAsync(string tipo);
    Task<IEnumerable<Refeicao>> GetByRestricaoAsync(int restricaoId);
}

public class RefeicaoRepository : Repository<Refeicao>, IRefeicaoRepository
{
    private readonly ApplicationDbContext _context;

    public RefeicaoRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Refeicao?> GetByNameAsync(string nome)
    {
        return await _context.Refeicoes
            .Include(r => r.Ingredientes)
            .Include(r => r.Restricoes)
            .FirstOrDefaultAsync(r => r.Nome == nome);
    }

    public async Task<IEnumerable<Refeicao>> GetByTipoAsync(string tipo)
    {
        return await _context.Refeicoes
            .Where(r => r.Tipo == tipo)
            .Include(r => r.Ingredientes)
            .Include(r => r.Restricoes)
            .ToListAsync();
    }

    public async Task<IEnumerable<Refeicao>> GetByRestricaoAsync(int restricaoId)
    {
        return await _context.Refeicoes
            .Where(r => r.Restricoes.Any(re => re.Id == restricaoId))
            .Include(r => r.Ingredientes)
            .Include(r => r.Restricoes)
            .ToListAsync();
    }
}
