namespace PROJETO.MEU.Models;

public class RestricaoAlimentar
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; // Lactose, Glúten, Vegetariano, Vegano, etc.
    public string Descricao { get; set; } = string.Empty;
    
    // Relacionamento
    public ICollection<Refeicao> Refeicoes { get; set; } = new List<Refeicao>();
}
