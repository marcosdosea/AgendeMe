using Core.DTO;

namespace Core.Service
{
    public interface ICidadaoService
    {
        int Create(Cidadao cidadao);
        void Edit(Cidadao cidadao);
        void Delete(int id);
        int AddProfissional(int idCidadao, int idPrefeitura, int idCargo);
        void EditProfissional(); //TODO
        void DeleteProfissional(int idCidadao, int idCargo, int idPrefeitura);
        Cidadao Get(int id);
        ProfissionalDTO GetProfissional(int idCidadao, string nomeCargo, string nomePrefeitura);
        IEnumerable<Cidadao> GetAll();
        IEnumerable<ProfissionalDTO> GetAllProfissional(int idPrefeitura);
    }
}
