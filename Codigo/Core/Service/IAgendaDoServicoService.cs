using AgendeMeWeb.Models;
using Core.DTO;

namespace Core.Service
{
    public interface IAgendaDoServicoService
    {
        int Create(Agendadoservico agendadoservico);
        void Edit(Agendadoservico agendadoservico);
        void Delete(int id);
        Agendadoservico Get(int id);
        IEnumerable<AgendadoservicoDTO> GetAll();
        public IEnumerable<AgendadoservicoDTO> GetByPage(DatatableDTO model, out int filteredResultsCount, out int totalResultsCount);
    }
}
