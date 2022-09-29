using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AgendeMeWeb.Models
{
    public class PrefeituraViewModel
    {

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Código da prefeitura é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "Nome da prefeitura deve ter entre 5 e 70 caracteres")]
        public string? Nome { get; set; }

        public string? Cnpj { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Nome da cidade deve ter entre 5 e 30 caracteres")]
        public string? Cidade { get; set; }

        public string? Bairro { get; set; }

        public string? Cep { get; set; }

        public string? Rua { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(7, MinimumLength = 1, ErrorMessage = "Número do edifício deve ter entre 1 e 7 caracteres")]
        public string? Numero { get; set; }

        public string? Icone { get; set; }
    }
}
