using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class DiaAgendamentoService : IDiaAgendamentoService
    {
        private readonly AgendeMeContext _context;

        public DiaAgendamentoService(AgendeMeContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Cria um Dia para um agendamento
        /// </summary>
        /// <param name="diaAgendamento"></param>
        /// <returns></returns>
        public int Create(Diaagendamento diaAgendamento)
        {
            _context.Add(diaAgendamento);
            _context.SaveChanges();
            return diaAgendamento.Id;
        }
        /// <summary>
        /// Deleta um dia para um agendamento
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var _diaAgendamento = _context.Orgaopublicos.Find(id);
            _context.Remove(_diaAgendamento);
            _context.SaveChanges();
        }
        /// <summary>
        /// Edita um dia de agendamento
        /// </summary>
        /// <param name="diaAgendamento"></param>
        public void Edit(Diaagendamento diaAgendamento)
        {
            _context.Update(diaAgendamento);
            _context.SaveChanges();
        }
        /// <summary>
        /// Consulta um dia de agendamento por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Diaagendamento Get(int id)
        {
            return _context.Diaagendamentos.Find(id);
        }
        /// <summary>
        /// Consulta os dados do agendamento para sua confirmacao
        /// </summary>
        /// <param name="id">Id do dia do agendamento</param>
        /// <returns>Dados do agendamento</returns>
        public ConfirmarAgendamentoDTO GetDadosAgendamento(int id)
        {
            var query = from diaAgendamento in _context.Diaagendamentos
                        where diaAgendamento.Id.Equals(id)
                        select new ConfirmarAgendamentoDTO
                        {
                            Id = diaAgendamento.Id,
                            IdServico = diaAgendamento.IdServicoPublico,
                            IconeServico = diaAgendamento.IdServicoPublicoNavigation.Icone,
                            NomeServico = diaAgendamento.IdServicoPublicoNavigation.Nome,
                            IdOrgao = diaAgendamento.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Id,
                            NomeOrgao = diaAgendamento.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Nome,
                            Bairro = diaAgendamento.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Bairro,
                            Rua = diaAgendamento.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Rua,
                            Numero = diaAgendamento.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Numero,
                            Complemento = diaAgendamento.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Complemento,
                            NomeDia = diaAgendamento.DiaSemana,
                            Data = diaAgendamento.Data,
                            Horario = string.Join(" às ", diaAgendamento.HorarioInicio, diaAgendamento.HorarioFim),
                            DataCadastro = DateTime.Now
                        };
            return query.AsNoTracking().First();
        }
        /// <summary>
        /// Consulta todos os dias agendamento
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Diaagendamento> GetAll()
        {
            return _context.Diaagendamentos.AsNoTracking();
        }
        /// <summary>
        /// Consulta todos os dias de agendamento para um servico
        /// </summary>
        /// <param name="idServico">Id do Servico</param>
        /// <returns>Todas os dias do servico</returns>
        public IEnumerable<AgendamentoDiasDTO> GetAllDiasByIdServico(int idServico)
        {
            var query = from diaAgendamento in _context.Diaagendamentos
                        where diaAgendamento.IdServicoPublico.Equals(idServico)
                        group diaAgendamento by new
                        {
                            diaAgendamento.DiaSemana,
                            diaAgendamento.Data,
                            diaAgendamento.IdServicoPublico
                        } into diaGroup
                        select new AgendamentoDiasDTO
                        {
                            DiaSemana = diaGroup.Key.DiaSemana,
                            Data = diaGroup.Key.Data,
                            IdServico = diaGroup.Key.IdServicoPublico,
                            Vagas = diaGroup.Sum(p => p.VagasAtendimento)
                        };
            return query.AsNoTracking();
        }
        /// <summary>
        /// Consulta todas as horas de um dia de agendamento para um servico
        /// </summary>
        /// <param name="idServico">Id do Servico</param>
        /// <param name="dia">Data do dia</param>
        /// <returns>Todas as horas do servico no dia</returns>
        public IEnumerable<AgendamentoHorasDTO> GetAllHorasByIdServicoAndDia(int idServico, DateTime dia)
        {
            var query = from diaAgendamento in _context.Diaagendamentos
                        where diaAgendamento.IdServicoPublico.Equals(idServico)
                        where diaAgendamento.Data.Date.Equals(dia.Date)
                        orderby diaAgendamento.Data
                        select new AgendamentoHorasDTO
                        {
                            Id = diaAgendamento.Id,
                            IdServico = diaAgendamento.IdServicoPublico,
                            HorarioInicio = diaAgendamento.HorarioInicio,
                            HorarioFim = diaAgendamento.HorarioFim,
                            Vagas = (diaAgendamento.VagasAtendimento - diaAgendamento.VagasAgendadas)
                        };
            return query.AsNoTracking();
        }
    }
}
