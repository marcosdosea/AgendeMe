using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Prefeitura
    {
        public Prefeitura()
        {
            Areadeservicos = new HashSet<Areadeservico>();
            Cargoprofissionalprefeituras = new HashSet<Cargoprofissionalprefeitura>();
            Cidadaos = new HashSet<Cidadao>();
            Orgaopublicos = new HashSet<Orgaopublico>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Icone { get; set; }

        public virtual ICollection<Areadeservico> Areadeservicos { get; set; }
        public virtual ICollection<Cargoprofissionalprefeitura> Cargoprofissionalprefeituras { get; set; }
        public virtual ICollection<Cidadao> Cidadaos { get; set; }
        public virtual ICollection<Orgaopublico> Orgaopublicos { get; set; }
    }
}
