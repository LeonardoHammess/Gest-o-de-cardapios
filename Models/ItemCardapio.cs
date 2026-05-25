namespace PROJETO.MEU.Models;

public class ItemCardapio
{
    public int Id { get; set; }
    public int CardapioId { get; set; }
    public int RefeicaoId { get; set; }
    public string DiaSemana { get; set; } = string.Empty; // Segunda, Terça, etc.
    public string Turno { get; set; } = string.Empty; // Almoço, Café da manhã, Café da tarde, etc.
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    
    // Relacionamentos
    public Cardapio Cardapio { get; set; } = null!;
    public Refeicao Refeicao { get; set; } = null!;
}
