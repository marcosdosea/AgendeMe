using System.ComponentModel.DataAnnotations;

namespace AgendeMeWeb.Models
{
    public class CargoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório o nome do cargo")]
        [Display(Name = "Cargo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo de descrição é obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
