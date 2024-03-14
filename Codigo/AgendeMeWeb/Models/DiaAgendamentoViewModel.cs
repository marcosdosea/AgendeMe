using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgendeMeWeb.Models
{
    public class DiaAgendamentoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A data é obrigatoria")]
        [Display(Name = "Dia")]
        public DateTime? Data { get; set; }
        public string? DiaSemana { get; set; }
        [Display(Name = "Horário de Início")]
        [Required(ErrorMessage = "O horário é obrigatorio")]
        public string? HorarioInicio { get; set; }
        [Display(Name = "Horário de Termíno")]
        [Required(ErrorMessage = "O horário é obrigatorio")]
        public string? HorarioFim { get; set; }
        [Display(Name = "Número de Vagas")]
        [Required(ErrorMessage = "Vaga é obrigatorio")]
        public int VagasAtendimento { get; set; }
        public int VagasAgendadas { get; set; }
        [Required(ErrorMessage = "Vaga de Retorno é obrigatorio")]
        public int VagasRetorno { get; set; }
        public int VagasAgendadasRetorno { get; set; }
        [Required(ErrorMessage = "O serviço é obrigatório")]
        public int IdServicoPublico { get; set; }
        [Display(Name = "Serviço")]
        public SelectList? ListaServicos{ get; set; }
    }
}
