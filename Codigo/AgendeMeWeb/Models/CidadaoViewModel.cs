using System.ComponentModel.DataAnnotations;

namespace AgendeMeWeb.Models
{
    public class CidadaoViewModel
    {
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatótio.")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "O nome do cidadão deve ter entre 5 e 70 caracteres")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [StringLength(15)]
        public string? Cpf { get; set; }

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date, ErrorMessage = "É necessário escolher uma data válida.")]
        [Required(ErrorMessage = "O campo  Data de nascimento é obrigatório.")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Cartão do SUS")]
        [Required(ErrorMessage = "O campo Cartão do SUS é obrigatório.")]
        [StringLength(20)]
        public string? Sus { get; set; }

        [Required(ErrorMessage = "O campo telefone é obrigatório.")]
        [StringLength(20)]
        public string? Telefone { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        [StringLength(10)]
        public string? Cep { get; set; }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "O estado é um campo obrigatório.")]
        [StringLength(2)]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        [StringLength(70)]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
        [StringLength(70)]
        public string? Bairro { get; set; }

        [Required(ErrorMessage = "O campo Rua é obrigatório.")]
        [StringLength(70)]
        public string? Rua { get; set; }

        [Display(Name = "Nº")]
        [Required(ErrorMessage = "O campo Número da casa é obrigatório. Caso não possua número informe: SN")]
        [StringLength(7)]
        public string? NumeroCasa { get; set; }

        [Required(ErrorMessage = "O campo Sexo é obrigatório.")]
        [StringLength(1)]
        public string? Sexo { get; set; }

        [Display(Name = "Tipo do cidadão")]
        public string? TipoCidadao { get; set; }
        public string? Complemento { get; set; }
        public int? IdOrgaoPublico { get; set; }
        public int? IdPrefeitura { get; set; }

        public Dictionary<string, string> SexoOptions { get; set; } = new()
        {
            { "Masculino", "M" },
            { "Feminino", "F" }
        };
    }
}
