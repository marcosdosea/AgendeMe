namespace Core.Service
{
    public interface IAgendaDoServicoService
    {
        int Create(Agendadoservico agendadoservico);
        void Edit(Agendadoservico agendadoservico);
        void Delete(int id);
        Agendadoservico Get(int id);
        IEnumerable<Agendadoservico> GetAll();

    }
}
