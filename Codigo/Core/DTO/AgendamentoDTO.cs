using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class AgendamentoDTO
    {
        [Display(Name = "Código")]
        public int Id { get; set; }
        public string? Tipo { get; set; }
        [Display(Name = "Situação")]
        public string? Situacao { get; set; }

        [Display(Name = "Serviço")]
        public string? NomeServico { get; set; }

        [Display(Name = "Local")]
        public string? OrgaoPublico { get; set; }

        public string? Bairro { get; set; }

        public string? Rua { get; set; }

        [Display(Name = "Número")]
        public string? Numero { get; set; }

        public string? Complemento { get; set; }

        public string Cidade { get; set; } = string.Empty;

        public string Cep { get; set; } = string.Empty;

        public DateTime Data { get; set; }

        [Display(Name = "Horário")]
        public string? Horario { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
    }

    public class AgendamentoPage
    {
        public IEnumerable<AgendamentoDTO>? Agendamentos {get; set;}
        public int PageSize {get; set;}
    }
}
