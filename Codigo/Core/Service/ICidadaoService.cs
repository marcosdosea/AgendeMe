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
        void DeletProfissional(int idCidadao, int idCargo, int idPrefeitura);
        Cidadao Get(int id);
        IEnumerable<ProfissionalDTO> GetProfissional(int id);
        IEnumerable<Cidadao> GetAll();
        IEnumerable<ProfissionalDTO> GetAllProfissional(int idPrefeitura);
        IEnumerable<CidadaoDTO> GetById(int idCidadao);
    }
}
