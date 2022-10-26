using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class ConfirmarAgendamentoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Serviço")]
        public string? NomeServico { get; set; }

        [Display(Name = "Local")]
        public string? OrgaoPublico { get; set; }

        public string? Bairro { get; set; }

        public string? Rua { get; set; }

        [Display(Name = "Número")]
        public string? Numero { get; set; }

        public string? Complemento { get; set; }

        public DateTime Data { get; set; }

        [Display(Name = "Horário")]
        public string? Horario { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
    }
}
