using System.ComponentModel.DataAnnotations;

namespace AgendeMeWeb.Models
{
    public class CargoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório o nome do cargo")]
        [Display(Name = "Título do cargo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo de descrição é obrigatório")]
        [Display(Name = "Descrição do cargo")]
        public string Descricao { get; set; }
    }
}
