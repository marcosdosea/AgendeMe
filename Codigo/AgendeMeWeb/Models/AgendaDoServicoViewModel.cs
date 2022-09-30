using System.ComponentModel.DataAnnotations;

namespace AgendeMeWeb.Models
{
    public class AgendaDoServicoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string DiaSemana { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "O Horário de fim deve ter entre 1 e 5 caracteres")]
        public string HorarioInicio { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "O Horário de fim deve ter entre 1 e 5 caracteres")]
        public string HorarioFim { get; set; }

        [Required(ErrorMessage = "O número de VagasAtendimento é obrigatório")]
        public int VagasAtendimento { get; set; }

        [Required(ErrorMessage = "O número de VagasRetorno é obrigatório")]
        public int VagasRetorno { get; set; }

        [Required(ErrorMessage = "O número do serviço público é obrigatório")]
        public int IdServicoPublico { get; set; }

        [Required(ErrorMessage = "O código do profissional é obrigatório")]
        public int IdProfissional { get; set; }
    }
}
