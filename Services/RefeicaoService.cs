using PROJETO.MEU.Models;
using PROJETO.MEU.Repositories;

namespace PROJETO.MEU.Services;

public interface IRefeicaoService
{
    Task<IEnumerable<Refeicao>> GetAllAsync();
    Task<Refeicao?> GetByIdAsync(int id);
    Task<Refeicao?> GetByNameAsync(string nome);
    Task<Refeicao> CreateAsync(string nome, string tipo, string descricao);
    Task<Refeicao> UpdateAsync(int id, string nome, string tipo, string descricao);
    Task DeleteAsync(int id);
    Task AddIngredientAsync(int refeicaoId, int ingredienteId);
    Task RemoveIngredientAsync(int refeicaoId, int ingredienteId);
    Task AddRestricaoAsync(int refeicaoId, int restricaoId);
    Task RemoveRestricaoAsync(int refeicaoId, int restricaoId);
    Task<IEnumerable<Refeicao>> GetByRestricaoAsync(int restricaoId);
}

public class RefeicaoService : IRefeicaoService
{
    private readonly IRefeicaoRepository _refeicaoRepository;
    private readonly IRepository<Ingrediente> _ingredienteRepository;
    private readonly IRepository<RestricaoAlimentar> _restricaoRepository;

    public RefeicaoService(
        IRefeicaoRepository refeicaoRepository,
        IRepository<Ingrediente> ingredienteRepository,
        IRepository<RestricaoAlimentar> restricaoRepository)
    {
        _refeicaoRepository = refeicaoRepository;
        _ingredienteRepository = ingredienteRepository;
        _restricaoRepository = restricaoRepository;
    }

    public async Task<IEnumerable<Refeicao>> GetAllAsync()
    {
        return await _refeicaoRepository.GetAllAsync();
    }

    public async Task<Refeicao?> GetByIdAsync(int id)
    {
        return await _refeicaoRepository.GetByIdAsync(id);
    }

    public async Task<Refeicao?> GetByNameAsync(string nome)
    {
        return await _refeicaoRepository.GetByNameAsync(nome);
    }

    public async Task<Refeicao> CreateAsync(string nome, string tipo, string descricao)
    {
        var refeicao = new Refeicao
        {
            Nome = nome,
            Tipo = tipo,
            Descricao = descricao
        };

        return await _refeicaoRepository.AddAsync(refeicao);
    }

    public async Task<Refeicao> UpdateAsync(int id, string nome, string tipo, string descricao)
    {
        var refeicao = await _refeicaoRepository.GetByIdAsync(id);
        if (refeicao == null)
            throw new Exception("Refeição não encontrada.");

        refeicao.Nome = nome;
        refeicao.Tipo = tipo;
        refeicao.Descricao = descricao;

        return await _refeicaoRepository.UpdateAsync(refeicao);
    }

    public async Task DeleteAsync(int id)
    {
        var refeicao = await _refeicaoRepository.GetByIdAsync(id);
        if (refeicao == null)
            throw new Exception("Refeição não encontrada.");

        await _refeicaoRepository.DeleteAsync(refeicao);
    }

    public async Task AddIngredientAsync(int refeicaoId, int ingredienteId)
    {
        var refeicao = await _refeicaoRepository.GetByIdAsync(refeicaoId);
        if (refeicao == null)
            throw new Exception("Refeição não encontrada.");

        var ingrediente = await _ingredienteRepository.GetByIdAsync(ingredienteId);
        if (ingrediente == null)
            throw new Exception("Ingrediente não encontrado.");

        if (!refeicao.Ingredientes.Any(i => i.Id == ingredienteId))
        {
            refeicao.Ingredientes.Add(ingrediente);
            await _refeicaoRepository.UpdateAsync(refeicao);
        }
    }

    public async Task RemoveIngredientAsync(int refeicaoId, int ingredienteId)
    {
        var refeicao = await _refeicaoRepository.GetByIdAsync(refeicaoId);
        if (refeicao == null)
            throw new Exception("Refeição não encontrada.");

        var ingrediente = refeicao.Ingredientes.FirstOrDefault(i => i.Id == ingredienteId);
        if (ingrediente != null)
        {
            refeicao.Ingredientes.Remove(ingrediente);
            await _refeicaoRepository.UpdateAsync(refeicao);
        }
    }

    public async Task AddRestricaoAsync(int refeicaoId, int restricaoId)
    {
        var refeicao = await _refeicaoRepository.GetByIdAsync(refeicaoId);
        if (refeicao == null)
            throw new Exception("Refeição não encontrada.");

        var restricao = await _restricaoRepository.GetByIdAsync(restricaoId);
        if (restricao == null)
            throw new Exception("Restrição alimentar não encontrada.");

        if (!refeicao.Restricoes.Any(r => r.Id == restricaoId))
        {
            refeicao.Restricoes.Add(restricao);
            await _refeicaoRepository.UpdateAsync(refeicao);
        }
    }

    public async Task RemoveRestricaoAsync(int refeicaoId, int restricaoId)
    {
        var refeicao = await _refeicaoRepository.GetByIdAsync(refeicaoId);
        if (refeicao == null)
            throw new Exception("Refeição não encontrada.");

        var restricao = refeicao.Restricoes.FirstOrDefault(r => r.Id == restricaoId);
        if (restricao != null)
        {
            refeicao.Restricoes.Remove(restricao);
            await _refeicaoRepository.UpdateAsync(refeicao);
        }
    }

    public async Task<IEnumerable<Refeicao>> GetByRestricaoAsync(int restricaoId)
    {
        return await _refeicaoRepository.GetByRestricaoAsync(restricaoId);
    }
}
