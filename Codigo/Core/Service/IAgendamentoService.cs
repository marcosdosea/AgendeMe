using Core.DTO;

namespace Core.Service
{
    public interface IAgendamentoService
    {

        Task<int> Create(Agendamento agendamento);
        void Edit(Agendamento agendamento);
        void Delete(int idAgendamento);
        Agendamento Get(int idAgendamento);
        IEnumerable<Agendamento> GetAll();
        AgendamentoDTO GetDados(int idAgendamento);

    }
}
