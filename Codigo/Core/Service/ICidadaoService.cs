namespace Core.Service
{
    public interface ICidadaoService
    {
        int Create(Cidadao cidadao);
        void Edit(Cidadao cidadao);
        void Delete(int id);
        int AddProfissional(int idCidadao, int idPrefeitura, int idCargo);
        void EditProfissional(); //TODO
        void DeletProfissional(); //TODO
        Cidadao Get(int id);
        IEnumerable<Cidadao> GetAll();
        IEnumerable<Cidadao> GetAllProfissional(int idPrefeitura);
    }
}
