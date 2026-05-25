using PROJETO.MEU.Models;
using PROJETO.MEU.Repositories;

namespace PROJETO.MEU.Services;

public interface IEstudanteService
{
    Task<IEnumerable<Estudante>> GetAllAsync();
    Task<Estudante?> GetByIdAsync(int id);
    Task<Estudante?> GetByMatriculaAsync(string matricula);
    Task<Estudante> CreateAsync(string nome, string matricula, string email);
    Task<Estudante> UpdateAsync(int id, string nome, string email);
    Task DeleteAsync(int id);
    Task AddRestricaoAsync(int estudanteId, int restricaoId);
    Task RemoveRestricaoAsync(int estudanteId, int restricaoId);
}

public class EstudanteService : IEstudanteService
{
    private readonly IEstudanteRepository _estudanteRepository;
    private readonly IRepository<RestricaoAlimentar> _restricaoRepository;

    public EstudanteService(
        IEstudanteRepository estudanteRepository,
        IRepository<RestricaoAlimentar> restricaoRepository)
    {
        _estudanteRepository = estudanteRepository;
        _restricaoRepository = restricaoRepository;
    }

    public async Task<IEnumerable<Estudante>> GetAllAsync()
    {
        return await _estudanteRepository.GetAllAsync();
    }

    public async Task<Estudante?> GetByIdAsync(int id)
    {
        return await _estudanteRepository.GetWithRestricoesByIdAsync(id);
    }

    public async Task<Estudante?> GetByMatriculaAsync(string matricula)
    {
        return await _estudanteRepository.GetByMatriculaAsync(matricula);
    }

    public async Task<Estudante> CreateAsync(string nome, string matricula, string email)
    {
        var estudante = new Estudante
        {
            Nome = nome,
            Matricula = matricula,
            Email = email
        };

        return await _estudanteRepository.AddAsync(estudante);
    }

    public async Task<Estudante> UpdateAsync(int id, string nome, string email)
    {
        var estudante = await _estudanteRepository.GetByIdAsync(id);
        if (estudante == null)
            throw new Exception("Estudante não encontrado.");

        estudante.Nome = nome;
        estudante.Email = email;

        return await _estudanteRepository.UpdateAsync(estudante);
    }

    public async Task DeleteAsync(int id)
    {
        var estudante = await _estudanteRepository.GetByIdAsync(id);
        if (estudante == null)
            throw new Exception("Estudante não encontrado.");

        await _estudanteRepository.DeleteAsync(estudante);
    }

    public async Task AddRestricaoAsync(int estudanteId, int restricaoId)
    {
        var estudante = await _estudanteRepository.GetWithRestricoesByIdAsync(estudanteId);
        if (estudante == null)
            throw new Exception("Estudante não encontrado.");

        var restricao = await _restricaoRepository.GetByIdAsync(restricaoId);
        if (restricao == null)
            throw new Exception("Restrição alimentar não encontrada.");

        if (!estudante.RestricoesDieteticas.Any(r => r.Id == restricaoId))
        {
            estudante.RestricoesDieteticas.Add(restricao);
            await _estudanteRepository.UpdateAsync(estudante);
        }
    }

    public async Task RemoveRestricaoAsync(int estudanteId, int restricaoId)
    {
        var estudante = await _estudanteRepository.GetWithRestricoesByIdAsync(estudanteId);
        if (estudante == null)
            throw new Exception("Estudante não encontrado.");

        var restricao = estudante.RestricoesDieteticas.FirstOrDefault(r => r.Id == restricaoId);
        if (restricao != null)
        {
            estudante.RestricoesDieteticas.Remove(restricao);
            await _estudanteRepository.UpdateAsync(estudante);
        }
    }
}
