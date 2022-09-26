using Core;
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
        public int Create(Agendamento agendamento)
        {
            agendamento.Tipo = "Agendamento";
            agendamento.Situacao = "Agendado";
            agendamento.DataCadastro = DateTime.Now;
            _context.Add(agendamento);
            _context.SaveChanges();
            return agendamento.Id;
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
        public IEnumerable<Agendamento> GetAll()
        {
            return _context.Agendamentos.AsNoTracking();
        }
    }
}
