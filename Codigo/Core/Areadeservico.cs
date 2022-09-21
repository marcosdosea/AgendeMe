using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Areadeservico
    {
        public Areadeservico()
        {
            Servicopublicos = new HashSet<Servicopublico>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdPrefeitura { get; set; }
        public string Icone { get; set; }

        public virtual Prefeitura IdPrefeituraNavigation { get; set; }
        public virtual ICollection<Servicopublico> Servicopublicos { get; set; }
    }
}
