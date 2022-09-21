using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Profissionalcargo
    {
        public int IdCargo { get; set; }
        public int IdProfissional { get; set; }

        public virtual Cargo IdCargoNavigation { get; set; }
        public virtual Cidadao IdProfissionalNavigation { get; set; }
    }
}
