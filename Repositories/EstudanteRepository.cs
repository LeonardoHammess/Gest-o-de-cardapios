using Microsoft.EntityFrameworkCore;
using PROJETO.MEU.Data;
using PROJETO.MEU.Models;

namespace PROJETO.MEU.Repositories;

public interface IEstudanteRepository : IRepository<Estudante>
{
    Task<Estudante?> GetByMatriculaAsync(string matricula);
    Task<Estudante?> GetByEmailAsync(string email);
    Task<Estudante?> GetWithRestricoesByIdAsync(int id);
}

public class EstudanteRepository : Repository<Estudante>, IEstudanteRepository
{
    private readonly ApplicationDbContext _context;

    public EstudanteRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Estudante?> GetByMatriculaAsync(string matricula)
    {
        return await _context.Estudantes
            .Include(e => e.RestricoesDieteticas)
            .FirstOrDefaultAsync(e => e.Matricula == matricula);
    }

    public async Task<Estudante?> GetByEmailAsync(string email)
    {
        return await _context.Estudantes
            .Include(e => e.RestricoesDieteticas)
            .FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<Estudante?> GetWithRestricoesByIdAsync(int id)
    {
        return await _context.Estudantes
            .Include(e => e.RestricoesDieteticas)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
