namespace Core.Service
{
    public interface ICidadaoService
    {
        int Create(Cidadao cidadao);
        void Edit(Cidadao cidadao);
        void Delete(int id);
        Cidadao Get(int id);
        IEnumerable<Cidadao> GetAll();
    }
}
