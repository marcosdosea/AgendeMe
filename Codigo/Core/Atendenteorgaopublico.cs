using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Atendenteorgaopublico
    {
        public int IdAtendente { get; set; }
        public int IdOrgaoPublico { get; set; }

        public virtual Cidadao IdAtendenteNavigation { get; set; }
        public virtual Orgaopublico IdOrgaoPublicoNavigation { get; set; }
    }
}
