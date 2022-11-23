

using System.ComponentModel;

namespace Core.DTO
{
    public class AgendadoservicoDTO 
    {
        public int Id { get; set; }

        [DisplayName("Dia")]
        public string? DiaSemana { get; set; }
        public string? HorarioInicio { get; set; }
        public string? HorarioFim { get; set; }

        [DisplayName("Agendamento")]
        public int VagasAtendimento { get; set; }

        [DisplayName("Retorno")]
        public int VagasRetorno { get; set; }
        
        [DisplayName("Serviço")]
        public string? NomeDoServicoPublico { get; set; }

        [DisplayName("Profissional")]
        public string? NomeDoProfissional { get; set; }
    }
}
