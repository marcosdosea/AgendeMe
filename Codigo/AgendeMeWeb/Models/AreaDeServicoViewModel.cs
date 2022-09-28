using System.ComponentModel.DataAnnotations;

namespace AgendeMeWeb.Models
{
    public class AreaDeServicoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome da área é obrigatório")]
        [StringLength(70, ErrorMessage = "O nome da área deve ter no máximo 70 caracteres")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int IdPrefeitura { get; set; }
        [Required(ErrorMessage = "O icone da área é obrigatório")]
        [StringLength(100, ErrorMessage = "O icone da área deve ter no máximo 100 caracteres")]
        public string? Icone { get; set; }
    }
}
