namespace Core.Service
{
    public interface IDiaAgendamentoService
    {
        int Create(Diaagendamento diaAgendamento);
        void Edit(Diaagendamento diaAgendamento);
        void Delete(int id);
        Diaagendamento Get(int id);
        IEnumerable<Diaagendamento> GetAll();
    }
}
