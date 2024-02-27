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
        public AgendamentoPage GetAllByUser(int id, int page)
        {
            var count = _context.Agendamentos.Where(a => a.IdCidadao == id).Count();

            var query =_context.Agendamentos.Where(a => a.IdCidadao == id).Select(
                 a => new AgendamentoDTO 
                 {
                    Id = a.Id,
                    Tipo = a.Tipo,
                    Situacao = a.Situacao,
                    NomeServico = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.Nome,
                    OrgaoPublico = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Nome,
                    Bairro = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Bairro,
                    Rua = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Rua,
                    Numero = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Numero,
                    Complemento = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Complemento,
                    Data = a.IdDiaAgendamentoNavigation.Data,
                    Horario = string.Join(" às ", a.IdDiaAgendamentoNavigation.HorarioInicio, a.IdDiaAgendamentoNavigation.HorarioFim),
                    DataCadastro = a.DataCadastro,
                    Cep = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Cep,
                    Cidade = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.IdPrefeituraNavigation.Cidade,
                 }
                 ).OrderByDescending(a => a.Id).Skip(4 * (page - 1)).Take(4);
            return new AgendamentoPage {Agendamentos = query, PageSize = (4 % (count == 0 ? 1 : count)) - 1};
        }

        public AgendamentoPage GetAllByCpf(string cpf, int page, int idOrgao)
        {
            var count = _context.Agendamentos.Where(a => a.IdCidadaoNavigation.Cpf == cpf 
                                                    && a.IdDiaAgendamentoNavigation.
                                                    IdServicoPublicoNavigation.
                                                    IdOrgaoPublicoNavigation.Id == idOrgao).Count();
            var query =_context.Agendamentos.Where(a => a.IdCidadaoNavigation.Cpf == cpf 
                                                   && a.IdDiaAgendamentoNavigation.
                                                   IdServicoPublicoNavigation.
                                                   IdOrgaoPublicoNavigation.Id == idOrgao).Select(
                 a => new AgendamentoDTO 
                 {
                    Id = a.Id,
                    Tipo = a.Tipo,
                    Situacao = a.Situacao,
                    NomeServico = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.Nome,
                    OrgaoPublico = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Nome,
                    Bairro = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Bairro,
                    Rua = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Rua,
                    Numero = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Numero,
                    Complemento = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Complemento,
                    Data = a.IdDiaAgendamentoNavigation.Data,
                    Horario = string.Join(" às ", a.IdDiaAgendamentoNavigation.HorarioInicio, a.IdDiaAgendamentoNavigation.HorarioFim),
                    DataCadastro = a.DataCadastro,
                    Cep = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.Cep,
                    Cidade = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.IdOrgaoPublicoNavigation.IdPrefeituraNavigation.Cidade,
                 }
                 ).OrderByDescending(a => a.Id).Skip(4 * (page - 1)).Take(4);
            return new AgendamentoPage {Agendamentos = query, PageSize = (4 % (count == 0 ? 1 : count)) - 1};
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

        public bool AtualizarStatus(int id, string cpf, string status)
        {
            var agendamento = _context.Agendamentos.Select(a => a)
                              .Where(a => a.Id == id && a.IdCidadaoNavigation.Cpf == cpf)
                              .FirstOrDefault();
            if (agendamento == null) 
            {
                return false;
            }
            agendamento.Situacao = status;
            agendamento.DataCadastro = DateTime.Now;
            try 
            {
                Edit(agendamento);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public PainelAtendimentoDTO GetAtendimentos(int idServico)
        {
            PainelAtendimentoDTO painel = new();

            var agendamentos = _context.Agendamentos
            .Where(a => a.IdDiaAgendamentoNavigation.IdServicoPublico == idServico && a.Situacao == "Aguardando Atendimento")
            .OrderBy(a => a.DataCadastro)
            .Select(a => new AgendamentosCard {
                Id = a.Id,
                NomeCidadao = a.IdCidadaoNavigation.Nome,
                NomeServico = a.IdDiaAgendamentoNavigation.IdServicoPublicoNavigation.Nome,
                BlocoHorario = string.Join(" às ", a.IdDiaAgendamentoNavigation.HorarioInicio, a.IdDiaAgendamentoNavigation.HorarioFim),
            });

            if (!agendamentos.Any()) {
                return painel;
            }

            painel.Horarios = agendamentos.Select( a => a.BlocoHorario).Distinct();

            painel.Atendendo = new [] { agendamentos.First() };
            painel.Proximos = agendamentos.Skip(1).Take(2); 
            painel.Aguardando = agendamentos.Skip(3).Take(4);

            return painel;
        }
    }
}
