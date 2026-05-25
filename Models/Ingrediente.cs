namespace PROJETO.MEU.Models;

public class Ingrediente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    
    // Relacionamento
    public ICollection<Refeicao> Refeicoes { get; set; } = new List<Refeicao>();
}
