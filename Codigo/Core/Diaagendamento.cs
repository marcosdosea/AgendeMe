using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Diaagendamento
    {
        public Diaagendamento()
        {
            Agendamentos = new HashSet<Agendamento>();
        }

        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string DiaSemana { get; set; }
        public string HorarioInicio { get; set; }
        public string HorarioFim { get; set; }
        public int VagasAtendimento { get; set; }
        public int VagasAgendadas { get; set; }
        public int VagasRetorno { get; set; }
        public int VagasAgendadasRetorno { get; set; }
        public int IdServicoPublico { get; set; }

        public virtual Servicopublico IdServicoPublicoNavigation { get; set; }
        public virtual ICollection<Agendamento> Agendamentos { get; set; }
    }
}
