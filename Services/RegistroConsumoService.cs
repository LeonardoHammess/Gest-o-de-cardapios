using PROJETO.MEU.Models;
using PROJETO.MEU.Repositories;

namespace PROJETO.MEU.Services;

public interface IRegistroConsumoService
{
    Task<IEnumerable<RegistroConsumo>> GetAllAsync();
    Task<RegistroConsumo> RegisterConsumoAsync(int estudanteId, int refeicaoId, string turno);
    Task<IEnumerable<RegistroConsumo>> GetByEstudanteIdAsync(int estudanteId);
    Task<IEnumerable<RegistroConsumo>> GetByDataAsync(DateTime data);
    Task<IEnumerable<RegistroConsumo>> GetByRefeicaoIdAsync(int refeicaoId);
}

public class RegistroConsumoService : IRegistroConsumoService
{
    private readonly IRegistroConsumoRepository _registroRepository;
    private readonly IEstudanteRepository _estudanteRepository;
    private readonly IRepository<Refeicao> _refeicaoRepository;

    public RegistroConsumoService(
        IRegistroConsumoRepository registroRepository,
        IEstudanteRepository estudanteRepository,
        IRepository<Refeicao> refeicaoRepository)
    {
        _registroRepository = registroRepository;
        _estudanteRepository = estudanteRepository;
        _refeicaoRepository = refeicaoRepository;
    }

    public async Task<IEnumerable<RegistroConsumo>> GetAllAsync()
    {
        return await _registroRepository.GetAllAsync();
    }

    public async Task<RegistroConsumo> RegisterConsumoAsync(int estudanteId, int refeicaoId, string turno)
    {
        var estudante = await _estudanteRepository.GetByIdAsync(estudanteId);
        if (estudante == null)
            throw new Exception("Estudante não encontrado.");

        var refeicao = await _refeicaoRepository.GetByIdAsync(refeicaoId);
        if (refeicao == null)
            throw new Exception("Refeição não encontrada.");

        var registro = new RegistroConsumo
        {
            EstudanteId = estudanteId,
            RefeicaoId = refeicaoId,
            Turno = turno,
            DataConsumo = DateTime.Now
        };

        return await _registroRepository.AddAsync(registro);
    }

    public async Task<IEnumerable<RegistroConsumo>> GetByEstudanteIdAsync(int estudanteId)
    {
        return await _registroRepository.GetByEstudanteIdAsync(estudanteId);
    }

    public async Task<IEnumerable<RegistroConsumo>> GetByDataAsync(DateTime data)
    {
        return await _registroRepository.GetByDataAsync(data);
    }

    public async Task<IEnumerable<RegistroConsumo>> GetByRefeicaoIdAsync(int refeicaoId)
    {
        return await _registroRepository.GetByRefeicaoIdAsync(refeicaoId);
    }
}
