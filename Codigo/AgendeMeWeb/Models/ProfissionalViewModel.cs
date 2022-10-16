using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AgendeMeWeb.Models
{
    public class ProfissionalViewModel
    {
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int IdCidadao { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatótio.")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "O nome do cidadão deve ter entre 5 e 70 caracteres")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string? Cpf { get; set; }

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date, ErrorMessage = "É necessário escolher uma data válida.")]
        [Required(ErrorMessage = "O campo  Data de nascimento é obrigatório.")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Cartão do SUS")]
        [Required(ErrorMessage = "O campo Cartão do SUS é obrigatório.")]
        public string? Sus { get; set; }

        [Required(ErrorMessage = "O campo telefone é obrigatório.")]
        public string? Telefone { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        public string? Cep { get; set; }

        [Required(ErrorMessage = "O estado é um campo obrigatório.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
        public string? Bairro { get; set; }

        [Required(ErrorMessage = "O campo Rua é obrigatório.")]
        public string? Rua { get; set; }

        [Display(Name = "Número da casa")]
        [Required(ErrorMessage = "O campo Número da casa é obrigatório. Caso não possua número informe: SN")]
        public string? NumeroCasa { get; set; }

        [Required(ErrorMessage = "O campo Sexo é obrigatório.")]
        public string? Sexo { get; set; }

        [Display(Name = "Tipo do cidadão")]
        [Required(ErrorMessage = "O campo Tipo do cidadão é obrigatório.")]
        public string? TipoCidadao { get; set; }
        public string? Complemento { get; set; }
        public int? IdOrgaoPublico { get; set; }
        public int? IdPrefeitura { get; set; }
        public int IdCargo { get; set; }
        public int IdProfissionalPrefeitura { get; set; }

        public SelectList? ListaCargos { get; set; }
        public SelectList? ListaPrefeituras { get; set; }
    }
}
