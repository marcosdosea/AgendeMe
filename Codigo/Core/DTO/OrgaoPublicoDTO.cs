using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class OrgaoPublicoDTO
    {
        public int IdServico { get; set; }

        public string? Nome { get; set; }
        [Display(Name = "Atendimento")]
        public string? Atendimento { get; set; }
        [Display(Name = "Bairro")]
        public string? Bairro { get; set; }
        [Display(Name = "Rua")]
        public string? Rua { get; set; }
        [Display(Name = "Número")]
        public string? Numero { get; set; }
    }
}
