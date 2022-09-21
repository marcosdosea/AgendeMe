﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Agendamento
    {
        public Agendamento()
        {
            InverseIdRetornoNavigation = new HashSet<Agendamento>();
        }

        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime Data { get; set; }
        public string HorarioInicio { get; set; }
        public string HorarioFim { get; set; }
        public int IdCidadao { get; set; }
        public int? IdAtendente { get; set; }
        public int? IdRetorno { get; set; }
        public int IdProfissional { get; set; }
        public int IdServicoPublico { get; set; }

        public virtual Cidadao IdAtendenteNavigation { get; set; }
        public virtual Cidadao IdCidadaoNavigation { get; set; }
        public virtual Cidadao IdProfissionalNavigation { get; set; }
        public virtual Agendamento IdRetornoNavigation { get; set; }
        public virtual Servicopublico IdServicoPublicoNavigation { get; set; }
        public virtual ICollection<Agendamento> InverseIdRetornoNavigation { get; set; }
    }
}
