using System;
using System.Collections.Generic;

#nullable disable

namespace Core
{
    public partial class Cidadao
    {
        public Cidadao()
        {
            Agendadoservicos = new HashSet<Agendadoservico>();
            AgendamentoIdAtendenteNavigations = new HashSet<Agendamento>();
            AgendamentoIdCidadaoNavigations = new HashSet<Agendamento>();
            AgendamentoIdProfissionalNavigations = new HashSet<Agendamento>();
            Atendenteorgaopublicos = new HashSet<Atendenteorgaopublico>();
            Profissionalcargos = new HashSet<Profissionalcargo>();
            Profissionalprefeituras = new HashSet<Profissionalprefeitura>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sus { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string NumeroCasa { get; set; }
        public string Sexo { get; set; }
        public string TipoCidadao { get; set; }
        public string Complemento { get; set; }
        public int? IdOrgaoPublico { get; set; }
        public int? IdPrefeitura { get; set; }

        public virtual Orgaopublico IdOrgaoPublicoNavigation { get; set; }
        public virtual Prefeitura IdPrefeituraNavigation { get; set; }
        public virtual ICollection<Agendadoservico> Agendadoservicos { get; set; }
        public virtual ICollection<Agendamento> AgendamentoIdAtendenteNavigations { get; set; }
        public virtual ICollection<Agendamento> AgendamentoIdCidadaoNavigations { get; set; }
        public virtual ICollection<Agendamento> AgendamentoIdProfissionalNavigations { get; set; }
        public virtual ICollection<Atendenteorgaopublico> Atendenteorgaopublicos { get; set; }
        public virtual ICollection<Profissionalcargo> Profissionalcargos { get; set; }
        public virtual ICollection<Profissionalprefeitura> Profissionalprefeituras { get; set; }
    }
}
