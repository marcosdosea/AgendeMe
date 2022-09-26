namespace AgendeMeWeb.Models
{
    public class AgendarServicoViewModel
    {
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public string? Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime Data { get; set; }
        public string? HorarioInicio { get; set; }
        public string? HorarioFim { get; set; }
        public int IdCidadao { get; set; }
        public int? IdAtendente { get; set; }
        public int? IdRetorno { get; set; }
        public int IdProfissional { get; set; }
        public int IdServicoPublico { get; set; }
    }
}
