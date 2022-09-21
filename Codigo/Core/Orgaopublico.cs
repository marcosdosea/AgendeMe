using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Orgaopublico
    {
        public Orgaopublico()
        {
            Atendenteorgaopublicos = new HashSet<Atendenteorgaopublico>();
            Cidadaos = new HashSet<Cidadao>();
            Servicopublicos = new HashSet<Servicopublico>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string HoraAbre { get; set; }
        public string HoraFecha { get; set; }
        public int IdPrefeitura { get; set; }

        public virtual Prefeitura IdPrefeituraNavigation { get; set; }
        public virtual ICollection<Atendenteorgaopublico> Atendenteorgaopublicos { get; set; }
        public virtual ICollection<Cidadao> Cidadaos { get; set; }
        public virtual ICollection<Servicopublico> Servicopublicos { get; set; }
    }
}
