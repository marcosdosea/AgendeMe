using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class AgendaDoServicoService : IAgendaDoServicoService
    {
        private readonly AgendeMeContext _context;

        public AgendaDoServicoService(AgendeMeContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Cria uma agenda do servico no banco de dados
        /// </summary>
        /// <param name="agendaDoServico">Dados da agenda do servico</param>
        /// <returns>Id da agenda do servico cadastrada</returns>
        public int Create(AgendaDoServico agendaDoServico)
        {
            _context.Add(agendaDoServico);
            _context.SaveChanges();
            return agendaDoServico.Id;
        }
        /// <summary>
        /// Deleta uma agenda do servico no banco de dados
        /// </summary>
        /// <param name="id">Id da agenda do servico</param>
        public void Delete(int id)
        {
            var _agendaDoServico = _context.Agendamentos.Find(id);
            _context.Remove(_agendaDoServico);
            _context.SaveChanges();
        }
        /// <summary>
        /// Edita uma agenda do servico no banco de dados
        /// </summary>
        /// <param name="agendaDoServico">Dados da agenda do servico</param>
        public void Edit(AgendaDoServico agendaDoServico)
        {
            _context.Update(agendaDoServico);
            _context.SaveChanges();
        }
        /// <summary>
        /// Consulta uma agenda do servico no banco de dados
        /// </summary>
        /// <param name="id">Id da agenda do servico</param>
        /// <returns>Dados da agenda do servico</returns>
        public AgendaDoServico Get(int id)
        {
            return _context.Agendadoservicos.Find(id);
        }
        /// <summary>
        /// Consulta todas as agendas de servico
        /// </summary>
        /// <returns>Dados de todas as agendas de servico</returns>
        public IEnumerable<AgendaDoServico> GetAll()
        {
            return _context.Agendadoservicos.AsNoTracking();
        }
    }
}
