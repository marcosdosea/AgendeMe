using Core.DTO;

namespace Core.Service
{
    public interface ICidadaoService
    {
        int Create(Cidadao cidadao);
        void Edit(Cidadao cidadao);
        void Delete(int id);
        int AddProfissional(int IdProfissional, int idPrefeitura, int idCargo);
        void EditProfissional(int IdProfissional, string idPrefeitura, string nomeCargo);
        void DeleteProfissional(int IdProfissional, string nomeCargo, string nomePrefeitura);
        Cidadao Get(int id);
        ProfissionalDTO GetProfissional(int IdProfissional, string nomeCargo, string nomePrefeitura);
        IEnumerable<Cidadao> GetAll();
        IEnumerable<ProfissionalDTO> GetAllProfissional();
        IEnumerable<CidadaoDTO> GetById(int idCidadao);
    }
}
