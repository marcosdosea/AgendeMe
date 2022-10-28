using Core;
using Core.DTO;
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
        public int Create(Agendadoservico agendaDoServico)
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
        public void Edit(Agendadoservico agendaDoServico)
        {
            _context.Update(agendaDoServico);
            _context.SaveChanges();
        }
        /// <summary>
        /// Consulta uma agenda do servico no banco de dados
        /// </summary>
        /// <param name="id">Id da agenda do servico</param>
        /// <returns>Dados da agenda do servico</returns>
        public Agendadoservico Get(int id)
        {
            return _context.Agendadoservicos.Find(id);
        }
        /// <summary>
        /// Consulta todas as agendas de servico
        /// </summary>
        /// <returns>Dados de todas as agendas de servico</returns>
        public IEnumerable<AgendadoservicoDTO> GetAll()
        {
            var query = from Agendadoservico in _context.Agendadoservicos
                        select new AgendadoservicoDTO
                        {
                            Id = Agendadoservico.Id,
                            DiaSemana = Agendadoservico.DiaSemana,
                            HorarioInicio = Agendadoservico.HorarioInicio,
                            HorarioFim = Agendadoservico.HorarioFim,
                            VagasAtendimento = Agendadoservico.VagasAtendimento,
                            VagasRetorno = Agendadoservico.VagasRetorno,
                            NomeDoServicoPublico = Agendadoservico.IdServicoPublicoNavigation.Nome,
                            NomeDoProfissional = Agendadoservico.IdProfissionalNavigation.Nome
                        };

            return query;
        }
    }
}
