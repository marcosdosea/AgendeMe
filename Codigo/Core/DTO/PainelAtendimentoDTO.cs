namespace Core.DTO;

public class PainelAtendimentoDTO
{
    public IEnumerable<string>? Horarios;
    public IEnumerable<AgendamentosCard>? Atendendo;
    public IEnumerable<AgendamentosCard>? Proximos;
    public IEnumerable<AgendamentosCard>? Aguardando;
    public string Background { get; set; } = "bg-at";
}

public class AgendamentosCard 
{
    public int Id { get; set; }
    public string NomeCidadao { get; set; } = string.Empty;
    public string NomeServico { get; set; } = string.Empty;
    public string BlocoHorario { get; set; } = string.Empty;
}