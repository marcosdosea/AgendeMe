using System.ComponentModel.DataAnnotations;

namespace Core.DTO;

public class AgendaDTO 
{
    public int Id { get; set; }
    public string Dia { get; set; } = string.Empty;

    [Display(Name = "Início")]
    public string Inicio { get; set; } = string.Empty;

    [Display(Name = "Termíno")]
    public string Termino { get; set; } = string.Empty;

    [Display(Name = "Total de Vagas")]
    public int NumVagas { get; set; }

    [Display(Name = "Agendamentos")]
    public int NumAgendado { get; set; }

    [Display(Name = "Disponíveis")]
    public int NumDis { get; set; }

    [Display(Name = "Serviço")]
    public string Servico { get; set; } = string.Empty;
}