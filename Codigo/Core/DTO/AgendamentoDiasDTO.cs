using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class AgendamentoDiasDTO
    {
        public int IdServico { get; set; }
        public string? DiaSemana { get; set; }
        public DateTime Data { get; set; }

        [Display(Name = "Vagas")]
        public int Vagas { get; set; }
    }
}
