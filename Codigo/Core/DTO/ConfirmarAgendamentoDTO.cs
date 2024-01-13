using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class ConfirmarAgendamentoDTO
    {
        public int Id { get; set; }
        public int IdServico { get; set; }
        public int IdArea { get; set; }

        [Display(Name = "Serviço")]
        public string? NomeServico { get; set; }
        public string? IconeServico { get; set; }
        public int IdOrgao { get; set; }
        [Display(Name = "Local")]
        public string? NomeOrgao { get; set; }

        public string? Bairro { get; set; }

        public string? Rua { get; set; }

        [Display(Name = "Número")]
        public string? Numero { get; set; }

        public string? Complemento { get; set; }

        public DateTime Data { get; set; }
        public string? NomeDia { get; set; }

        [Display(Name = "Horário")]
        public string? Horario { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
    }
}
