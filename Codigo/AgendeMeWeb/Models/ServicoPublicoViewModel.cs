using System.ComponentModel.DataAnnotations;

namespace AgendeMeWeb.Models
{
    public class ServicoPublicoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do serviço público é obrigatório")]
        [StringLength(70, ErrorMessage = "O nome da área deve ter no máximo 70 caracteres")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int IdArea { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int IdOrgaoPublico { get; set; }
        [Required(ErrorMessage = "O icone do serviço público é obrigatório")]
        [StringLength(70, ErrorMessage = "O icone da área deve ter no máximo 100 caracteres")]
        public string? Icone { get; set; }
    }
}
