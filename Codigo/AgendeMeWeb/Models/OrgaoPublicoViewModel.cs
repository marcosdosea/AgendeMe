using System.ComponentModel.DataAnnotations;

namespace AgendeMeWeb.Models
{
    public class OrgaoPublicoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatótio.")]
        [StringLength(70, ErrorMessage = "O nome do órgão público deve ter no máximo 70 caracteres")]
        public string? Nome { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O campo Bairro é obrigatótio.")]
        [StringLength(70, ErrorMessage = "O bairro deve ter no máximo 70 caracteres")]
        public string? Bairro { get; set; }

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "O campo Rua é obrigatótio.")]
        [StringLength(70, ErrorMessage = "A rua deve ter no máximo 70 caracteres")]
        public string? Rua { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O campo Número é obrigatório.")]
        [StringLength(7, ErrorMessage = "O Número deve ter no máximo 7 caracteres")]
        public string? Numero { get; set; }

        [StringLength(70, ErrorMessage = "O Complemento deve ter no máximo 70 caracteres")]
        public string? Complemento { get; set; }

        [Display(Name = "CEP")]
        [StringLength(10, ErrorMessage = "O CEP deve ter no máximo 10 caracteres")]
        public string? Cep { get; set; }

        [Display(Name = "Horário de abertura")]
        [Required(ErrorMessage = "O campo Horário de abertura é obrigatório.")]
        [StringLength(5, ErrorMessage = "O Horário de abertura deve ter no máximo 5 caracteres")]
        public string? HoraAbre { get; set; }

        [Display(Name = "Horário de fechamento")]
        [Required(ErrorMessage = "O campo Horário de fechamento é obrigatório.")]
        [StringLength(5, ErrorMessage = "O Horário de fechamento deve ter no máximo 5 caracteres")]
        public string? HoraFecha { get; set; }
        public int IdPrefeitura { get; set; }
    }
}
