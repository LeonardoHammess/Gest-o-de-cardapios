using Microsoft.EntityFrameworkCore;
using PROJETO.MEU.Data;
using PROJETO.MEU.Models;

namespace PROJETO.MEU.Repositories;

public interface ICardapioRepository : IRepository<Cardapio>
{
    Task<Cardapio?> GetByWeekAsync(DateTime dataInicio);
    Task<IEnumerable<ItemCardapio>> GetItemsByCardapioIdAsync(int cardapioId);
    Task<ItemCardapio?> GetItemByIdAsync(int itemId);
    Task<bool> ExistsRefeicaoAtDayTurnoAsync(int cardapioId, string dia, string turno, int refeicaoId);
}

public class CardapioRepository : Repository<Cardapio>, ICardapioRepository
{
    private readonly ApplicationDbContext _context;

    public CardapioRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Cardapio?> GetByWeekAsync(DateTime dataInicio)
    {
        return await _context.Cardapios
            .Include(c => c.Itens)
            .ThenInclude(i => i.Refeicao)
            .ThenInclude(r => r.Restricoes)
            .FirstOrDefaultAsync(c => c.DataInicio == dataInicio);
    }

    public async Task<IEnumerable<ItemCardapio>> GetItemsByCardapioIdAsync(int cardapioId)
    {
        return await _context.ItemCardapios
            .Where(i => i.CardapioId == cardapioId)
            .Include(i => i.Refeicao)
            .ThenInclude(r => r.Ingredientes)
            .Include(i => i.Refeicao)
            .ThenInclude(r => r.Restricoes)
            .ToListAsync();
    }

    public async Task<ItemCardapio?> GetItemByIdAsync(int itemId)
    {
        return await _context.ItemCardapios
            .Include(i => i.Refeicao)
            .ThenInclude(r => r.Ingredientes)
            .Include(i => i.Refeicao)
            .ThenInclude(r => r.Restricoes)
            .FirstOrDefaultAsync(i => i.Id == itemId);
    }

    public async Task<bool> ExistsRefeicaoAtDayTurnoAsync(int cardapioId, string dia, string turno, int refeicaoId)
    {
        return await _context.ItemCardapios
            .AnyAsync(i => i.CardapioId == cardapioId 
                && i.DiaSemana == dia 
                && i.Turno == turno 
                && i.RefeicaoId == refeicaoId);
    }
}
