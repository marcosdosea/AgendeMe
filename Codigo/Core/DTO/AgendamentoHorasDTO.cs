using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class AgendamentoHorasDTO
    {
        public int Id { get; set; }
        public int IdServico { get; set; }
        public string? HorarioInicio { get; set; }

        public string? HorarioFim { get; set; }

        [Display(Name = "Vagas")]
        public int Vagas { get; set; }
    }
}
