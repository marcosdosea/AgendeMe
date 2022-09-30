namespace Core.Service
{
    public interface IServicoPublicoService
    {
        int Create(Servicopublico servicoPublico);
        void Edit(Servicopublico servicoPublico);
        void Delete(int id);
        Servicopublico Get(int id);
        IEnumerable<Servicopublico> GetAll();
    }
}
