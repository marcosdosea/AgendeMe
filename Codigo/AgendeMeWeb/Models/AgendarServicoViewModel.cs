using System.ComponentModel.DataAnnotations;

namespace AgendeMeWeb.Models
{
    public class AgendarServicoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? Tipo { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string? Situacao { get; set; }

        [Display(Name = "Data do Cadastro")]
        [DataType(DataType.Date, ErrorMessage = "Data válida requerida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data do Agendamento")]
        [DataType(DataType.Date, ErrorMessage = "Data válida requerida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "O horário de início deve ter entre 1 e 5 caracteres")]
        public string? HorarioInicio { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "O horário de fim deve ter entre 1 e 5 caracteres")]
        public string? HorarioFim { get; set; }

        [Required(ErrorMessage = "Código do cidadão é obrigatório")]
        public int IdCidadao { get; set; }

        public int? IdAtendente { get; set; }
        public int? IdRetorno { get; set; }

        [Required(ErrorMessage = "Código do profissional é obrigatório")]
        public int IdProfissional { get; set; }

        public int IdServicoPublico { get; set; }
    }
}
