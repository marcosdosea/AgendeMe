using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly AgendeMeContext _context;

        public AgendamentoService(AgendeMeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um agendamento no banco de dados
        /// </summary>
        /// <param name="agendamento">Dados do agendamento</param>
        /// <returns>Id do agendamento</returns>
        public async Task<int> Create(Agendamento agendamento)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (agendamento.IdRetorno != null)
                    agendamento.Tipo = "Retorno";
                else
                    agendamento.Tipo = "Agendamento";
                agendamento.Situacao = "Agendado";

                await _context.AddAsync(agendamento);

                var diaAgendamento = await _context.Diaagendamentos.FindAsync(agendamento.IdDiaAgendamento);
                if (diaAgendamento.VagasAtendimento > diaAgendamento.VagasAgendadas)
                {
                    diaAgendamento.VagasAgendadas += 1;
                    _context.Update(diaAgendamento);
                }
                else
                {
                    await transaction.RollbackAsync();
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return agendamento.Id;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Deleta um agendamento no banco de dados
        /// </summary>
        /// <param name="idAgendamento">Id do agendamento</param>
        public void Delete(int idAgendamento)
        {
            var _agendamento = _context.Agendamentos.Find(idAgendamento);
            _context.Remove(_agendamento);
            _context.SaveChanges();
        }

        /// <summary>
        /// Edita um agendamento no banco de dados
        /// </summary>
        /// <param name="agendamento">Dados do agendamento</param>
        public void Edit(Agendamento agendamento)
        {
            _context.Update(agendamento);
            _context.SaveChanges();
        }
        /// <summary>
        /// Consulta um agendamento no banco de dados
        /// </summary>
        /// <param name="idAgendamento"></param>
        /// <returns>Dados do agendamento</returns>
        public Agendamento Get(int idAgendamento)
        {
            return _context.Agendamentos.Find(idAgendamento);
        }
        /// <summary>
        /// Consulta todos os agendamentos no banco de dados
        /// </summary>
        /// <returns>Dados de todos os agendamentos</returns>
        public IEnumerable<Agendamento> GetAllByUser(int id)
        {
            return _context.Agendamentos.Where(a => a.IdCidadao == id).AsNoTracking();
        }

        public AgendamentoDTO GetDados(int id)
        {
            var query = from agendamento in _context.Agendamentos
                        where agendamento.Id.Equals(id)
                        select new AgendamentoDTO
                        {
                            Id = agendamento.Id,
                            Tipo = agendamento.Tipo,
                            Situacao = agendamento.Situacao,
                            NomeServico = agendamento.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.Nome,
                            OrgaoPublico = agendamento.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Nome,
                            Bairro = agendamento.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Bairro,
                            Rua = agendamento.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Rua,
                            Numero = agendamento.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Numero,
                            Complemento = agendamento.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Complemento,
                            Data = agendamento.IdDiaAgendamentoNavigation.Data,
                            Horario = string.Join(" às ", agendamento.IdDiaAgendamentoNavigation.HorarioInicio, agendamento.IdDiaAgendamentoNavigation.HorarioFim),
                            DataCadastro = agendamento.DataCadastro
                        };
            return query.AsNoTracking().First();
        }
    }
}
