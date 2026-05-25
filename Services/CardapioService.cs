using PROJETO.MEU.Models;
using PROJETO.MEU.Repositories;

namespace PROJETO.MEU.Services;

public interface ICardapioService
{
    Task<IEnumerable<Cardapio>> GetAllAsync();
    Task<Cardapio?> GetByIdAsync(int id);
    Task<Cardapio> CreateAsync(string semana, DateTime dataInicio, DateTime dataFim);
    Task AddItemAsync(int cardapioId, int refeicaoId, string dia, string turno);
    Task RemoveItemAsync(int itemId);
    Task<IEnumerable<ItemCardapio>> GetItemsByCardapioIdAsync(int cardapioId);
    Task<bool> VerifyDuplicateAsync(int cardapioId, string dia, string turno, int refeicaoId);
}

public class CardapioService : ICardapioService
{
    private readonly ICardapioRepository _cardapioRepository;
    private readonly IRepository<Refeicao> _refeicaoRepository;

    public CardapioService(
        ICardapioRepository cardapioRepository,
        IRepository<Refeicao> refeicaoRepository)
    {
        _cardapioRepository = cardapioRepository;
        _refeicaoRepository = refeicaoRepository;
    }

    public async Task<IEnumerable<Cardapio>> GetAllAsync()
    {
        return await _cardapioRepository.GetAllAsync();
    }

    public async Task<Cardapio?> GetByIdAsync(int id)
    {
        return await _cardapioRepository.GetByIdAsync(id);
    }

    public async Task<Cardapio> CreateAsync(string semana, DateTime dataInicio, DateTime dataFim)
    {
        var cardapio = new Cardapio
        {
            Semana = semana,
            DataInicio = dataInicio,
            DataFim = dataFim,
            DataCriacao = DateTime.Now
        };

        return await _cardapioRepository.AddAsync(cardapio);
    }

    public async Task AddItemAsync(int cardapioId, int refeicaoId, string dia, string turno)
    {
        var cardapio = await _cardapioRepository.GetByIdAsync(cardapioId);
        if (cardapio == null)
            throw new Exception("Cardápio não encontrado.");

        var refeicao = await _refeicaoRepository.GetByIdAsync(refeicaoId);
        if (refeicao == null)
            throw new Exception("Refeição não encontrada.");

        // Verificar duplicata
        var existe = await _cardapioRepository.ExistsRefeicaoAtDayTurnoAsync(cardapioId, dia, turno, refeicaoId);
        if (existe)
            throw new Exception("Esta refeição já está cadastrada neste dia e turno.");

        var item = new ItemCardapio
        {
            CardapioId = cardapioId,
            RefeicaoId = refeicaoId,
            DiaSemana = dia,
            Turno = turno,
            DataCriacao = DateTime.Now
        };

        cardapio.Itens.Add(item);
        await _cardapioRepository.UpdateAsync(cardapio);
    }

    public async Task RemoveItemAsync(int itemId)
    {
        var item = await _cardapioRepository.GetItemByIdAsync(itemId);
        if (item == null)
            throw new Exception("Item do cardápio não encontrado.");

        var cardapio = await _cardapioRepository.GetByIdAsync(item.CardapioId);
        if (cardapio != null)
        {
            cardapio.Itens.Remove(item);
            await _cardapioRepository.UpdateAsync(cardapio);
        }
    }

    public async Task<IEnumerable<ItemCardapio>> GetItemsByCardapioIdAsync(int cardapioId)
    {
        return await _cardapioRepository.GetItemsByCardapioIdAsync(cardapioId);
    }

    public async Task<bool> VerifyDuplicateAsync(int cardapioId, string dia, string turno, int refeicaoId)
    {
        return await _cardapioRepository.ExistsRefeicaoAtDayTurnoAsync(cardapioId, dia, turno, refeicaoId);
    }
}
