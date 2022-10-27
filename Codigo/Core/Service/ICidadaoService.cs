using Core.DTO;

namespace Core.Service
{
    public interface ICidadaoService
    {
        int Create(Cidadao cidadao);
        void Edit(Cidadao cidadao);
        void Delete(int id);
        int AddProfissional(int IdProfissional, int idPrefeitura, int idCargo);
        void EditProfissional(Cargoprofissionalprefeitura profissional);
        void DeleteProfissional(Cargoprofissionalprefeitura profissional);
        Cidadao Get(int id);
        Cargoprofissionalprefeitura GetProfissional(int IdProfissional, int IdCargo, int IdPrefeitura);
        IEnumerable<Cidadao> GetAll();
        IEnumerable<ProfissionalDTO> GetAllProfissional();
        IEnumerable<CidadaoDTO> GetById(int idCidadao);
    }
}
