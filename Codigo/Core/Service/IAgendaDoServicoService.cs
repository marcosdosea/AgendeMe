namespace Core.Service
{
    public interface IAgendaDoServicoService
    {
        int Create(AgendaDoServico agendadoservico);
        void Edit(AgendaDoServico agendadoservico);
        void Delete(int id);
        AgendaDoServico Get(int id);
        IEnumerable<AgendaDoServico> GetAll();

    }
}
