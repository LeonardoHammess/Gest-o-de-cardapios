namespace PROJETO.MEU.Models;

public class Refeicao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; // Almoço, Café da manhã, Lanche, etc.
    public string Descricao { get; set; } = string.Empty;
    
    // Relacionamentos
    public ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
    public ICollection<RestricaoAlimentar> Restricoes { get; set; } = new List<RestricaoAlimentar>();
    public ICollection<ItemCardapio> ItemCardapios { get; set; } = new List<ItemCardapio>();
    public ICollection<RegistroConsumo> Consumos { get; set; } = new List<RegistroConsumo>();
}
