using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Agendadoservico
    {
        public int Id { get; set; }
        public string DiaSemana { get; set; }
        public string HorarioInicio { get; set; }
        public string HorarioFim { get; set; }
        public int VagasAtendimento { get; set; }
        public int VagasRetorno { get; set; }
        public int IdServicoPublico { get; set; }
        public int IdProfissional { get; set; }

        public virtual Cidadao IdProfissionalNavigation { get; set; }
        public virtual Servicopublico IdServicoPublicoNavigation { get; set; }
    }
}
