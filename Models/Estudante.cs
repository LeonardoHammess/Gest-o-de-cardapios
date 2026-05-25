namespace PROJETO.MEU.Models;

public class Estudante
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    // Preferências e restrições
    public ICollection<RestricaoAlimentar> RestricoesDieteticas { get; set; } = new List<RestricaoAlimentar>();
    
    // Relacionamento
    public ICollection<RegistroConsumo> Consumos { get; set; } = new List<RegistroConsumo>();
}
