using Core.DTO;

namespace Core.Service
{
    public interface IDiaAgendamentoService
    {
        int Create(Diaagendamento diaAgendamento);
        void Edit(Diaagendamento diaAgendamento);
        void Delete(int id);
        Diaagendamento Get(int id);
        ConfirmarAgendamentoDTO GetDadosAgendamento(int id);
        IEnumerable<Diaagendamento> GetAll();
        public IEnumerable<AgendamentoDiasDTO> GetAllDiasByIdServico(int idServico);
        public IEnumerable<AgendamentoHorasDTO> GetAllHorasByIdServicoAndDia(int idServico, DateTime dia);
    }
}
