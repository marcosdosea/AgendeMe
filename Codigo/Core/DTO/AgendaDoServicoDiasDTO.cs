using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class AgendaDoServicoDiasDTO
    {
        public int IdServico { get; set; }
        public string? DiaSemana { get; set; }

        [Display(Name = "Vagas")]
        public int Vagas { get; set; }
    }
}
