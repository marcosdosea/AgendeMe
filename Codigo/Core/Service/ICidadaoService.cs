using Core.DTO;

namespace Core.Service
{
    public interface ICidadaoService
    {
        int Create(Cidadao cidadao);
        void Edit(Cidadao cidadao);
        void Delete(int id);
        int AddProfissional(int idCidadao, int idPrefeitura, int idCargo);
        void EditProfissional(int idCidadao, string idPrefeitura, string nomeCargo);
        void DeleteProfissional(int idCidadao, string nomeCargo, string nomePrefeitura);
        Cidadao Get(int id);
        ProfissionalDTO GetProfissional(int idCidadao, string nomeCargo, string nomePrefeitura);
        IEnumerable<Cidadao> GetAll();
        IEnumerable<ProfissionalDTO> GetAllProfissional();
        IEnumerable<CidadaoDTO> GetById(int idCidadao);
    }
}
