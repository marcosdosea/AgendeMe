using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Profissionalprefeitura
    {
        public int IdProfissional { get; set; }
        public int IdPrefeitura { get; set; }

        public virtual Prefeitura IdPrefeituraNavigation { get; set; }
        public virtual Cidadao IdProfissionalNavigation { get; set; }
    }
}
