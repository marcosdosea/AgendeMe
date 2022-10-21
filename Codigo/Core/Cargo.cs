using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Cargo
    {
        public Cargo()
        {
            Cargoprofissionalprefeituras = new HashSet<Cargoprofissionalprefeitura>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Cargoprofissionalprefeitura> Cargoprofissionalprefeituras { get; set; }
    }
}
