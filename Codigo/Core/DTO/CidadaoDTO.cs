using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class CidadaoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "CPF")]
        public string? Cpf { get; set; }

        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Nº Cartão do SUS")]
        public string? Sus { get; set; }
        public string? Telefone { get; set; }

        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Display(Name = "CEP")]
        public string? Cep { get; set; }

        [Display(Name = "UF")]
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public string? Bairro { get; set; }
        public string? Rua { get; set; }

        [Display(Name = "Número da casa")]
        public string? NumeroCasa { get; set; }
        public string? Sexo { get; set; }
        public string? Complemento { get; set; }
        public int? IdPrefeitura { get; set; }
        public Prefeitura? Prefeitura { get; set; }
        public string Papel { get; set; } = string.Empty;
        public int IdOrgao { get; set; }
    }
}
