namespace Core.Service
{
    public interface IAgendamentoService
    {

        int Create(Agendamento agendamento);
        void Edit(Agendamento agendamento);
        void Delete(int idAgendamento);
        Agendamento Get(int idAgendamento);
        IEnumerable<Agendamento> GetAll();

    }
}
