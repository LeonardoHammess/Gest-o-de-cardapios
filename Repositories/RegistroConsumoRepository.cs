using Microsoft.EntityFrameworkCore;
using PROJETO.MEU.Data;
using PROJETO.MEU.Models;

namespace PROJETO.MEU.Repositories;

public interface IRegistroConsumoRepository : IRepository<RegistroConsumo>
{
    Task<IEnumerable<RegistroConsumo>> GetByEstudanteIdAsync(int estudanteId);
    Task<IEnumerable<RegistroConsumo>> GetByDataAsync(DateTime data);
    Task<IEnumerable<RegistroConsumo>> GetByRefeicaoIdAsync(int refeicaoId);
}

public class RegistroConsumoRepository : Repository<RegistroConsumo>, IRegistroConsumoRepository
{
    private readonly ApplicationDbContext _context;

    public RegistroConsumoRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RegistroConsumo>> GetByEstudanteIdAsync(int estudanteId)
    {
        return await _context.RegistrosConsumo
            .Where(rc => rc.EstudanteId == estudanteId)
            .Include(rc => rc.Estudante)
            .Include(rc => rc.Refeicao)
            .OrderByDescending(rc => rc.DataConsumo)
            .ToListAsync();
    }

    public async Task<IEnumerable<RegistroConsumo>> GetByDataAsync(DateTime data)
    {
        var dataInicio = data.Date;
        var dataFim = dataInicio.AddDays(1);

        return await _context.RegistrosConsumo
            .Where(rc => rc.DataConsumo >= dataInicio && rc.DataConsumo < dataFim)
            .Include(rc => rc.Estudante)
            .Include(rc => rc.Refeicao)
            .ToListAsync();
    }

    public async Task<IEnumerable<RegistroConsumo>> GetByRefeicaoIdAsync(int refeicaoId)
    {
        return await _context.RegistrosConsumo
            .Where(rc => rc.RefeicaoId == refeicaoId)
            .Include(rc => rc.Estudante)
            .OrderByDescending(rc => rc.DataConsumo)
            .ToListAsync();
    }
}
