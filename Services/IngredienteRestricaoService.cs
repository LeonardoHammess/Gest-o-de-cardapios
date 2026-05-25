using PROJETO.MEU.Models;
using PROJETO.MEU.Repositories;

namespace PROJETO.MEU.Services;

public interface IIngredienteService
{
    Task<IEnumerable<Ingrediente>> GetAllAsync();
    Task<Ingrediente?> GetByIdAsync(int id);
    Task<Ingrediente> CreateAsync(string nome, string descricao);
    Task<Ingrediente> UpdateAsync(int id, string nome, string descricao);
    Task DeleteAsync(int id);
}

public class IngredienteService : IIngredienteService
{
    private readonly IRepository<Ingrediente> _repository;

    public IngredienteService(IRepository<Ingrediente> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Ingrediente>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Ingrediente?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Ingrediente> CreateAsync(string nome, string descricao)
    {
        var ingrediente = new Ingrediente
        {
            Nome = nome,
            Descricao = descricao
        };

        return await _repository.AddAsync(ingrediente);
    }

    public async Task<Ingrediente> UpdateAsync(int id, string nome, string descricao)
    {
        var ingrediente = await _repository.GetByIdAsync(id);
        if (ingrediente == null)
            throw new Exception("Ingrediente não encontrado.");

        ingrediente.Nome = nome;
        ingrediente.Descricao = descricao;

        return await _repository.UpdateAsync(ingrediente);
    }

    public async Task DeleteAsync(int id)
    {
        var ingrediente = await _repository.GetByIdAsync(id);
        if (ingrediente == null)
            throw new Exception("Ingrediente não encontrado.");

        await _repository.DeleteAsync(ingrediente);
    }
}

public interface IRestricaoAlimentarService
{
    Task<IEnumerable<RestricaoAlimentar>> GetAllAsync();
    Task<RestricaoAlimentar?> GetByIdAsync(int id);
    Task<RestricaoAlimentar> CreateAsync(string nome, string descricao);
    Task<RestricaoAlimentar> UpdateAsync(int id, string nome, string descricao);
    Task DeleteAsync(int id);
}

public class RestricaoAlimentarService : IRestricaoAlimentarService
{
    private readonly IRepository<RestricaoAlimentar> _repository;

    public RestricaoAlimentarService(IRepository<RestricaoAlimentar> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RestricaoAlimentar>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<RestricaoAlimentar?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<RestricaoAlimentar> CreateAsync(string nome, string descricao)
    {
        var restricao = new RestricaoAlimentar
        {
            Nome = nome,
            Descricao = descricao
        };

        return await _repository.AddAsync(restricao);
    }

    public async Task<RestricaoAlimentar> UpdateAsync(int id, string nome, string descricao)
    {
        var restricao = await _repository.GetByIdAsync(id);
        if (restricao == null)
            throw new Exception("Restrição alimentar não encontrada.");

        restricao.Nome = nome;
        restricao.Descricao = descricao;

        return await _repository.UpdateAsync(restricao);
    }

    public async Task DeleteAsync(int id)
    {
        var restricao = await _repository.GetByIdAsync(id);
        if (restricao == null)
            throw new Exception("Restrição alimentar não encontrada.");

        await _repository.DeleteAsync(restricao);
    }
}
