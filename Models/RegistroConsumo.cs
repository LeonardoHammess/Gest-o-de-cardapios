namespace PROJETO.MEU.Models;

public class RegistroConsumo
{
    public int Id { get; set; }
    public int EstudanteId { get; set; }
    public int RefeicaoId { get; set; }
    public DateTime DataConsumo { get; set; } = DateTime.Now;
    public string Turno { get; set; } = string.Empty;
    
    // Relacionamentos
    public Estudante Estudante { get; set; } = null!;
    public Refeicao Refeicao { get; set; } = null!;
}
