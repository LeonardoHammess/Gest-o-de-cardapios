namespace PROJETO.MEU.Models;

public class Cardapio
{
    public int Id { get; set; }
    public string Semana { get; set; } = string.Empty; // Ex: "2026-05-25 a 2026-05-31"
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    
    // Relacionamento
    public ICollection<ItemCardapio> Itens { get; set; } = new List<ItemCardapio>();
}
