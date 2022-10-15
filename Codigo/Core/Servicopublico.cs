using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Servicopublico
    {
        public Servicopublico()
        {
            Agendadoservicos = new HashSet<Agendadoservico>();
            Agendamentos = new HashSet<Agendamento>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdArea { get; set; }
        public int IdOrgaoPublico { get; set; }
        public string Icone { get; set; }

        public virtual Areadeservico IdAreaNavigation { get; set; }
        public virtual Orgaopublico IdOrgaoPublicoNavigation { get; set; }
        public virtual ICollection<Agendadoservico> Agendadoservicos { get; set; }
        public virtual ICollection<Agendamento> Agendamentos { get; set; }
    }
}
