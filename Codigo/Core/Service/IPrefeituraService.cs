using Core.DTO;

namespace Core.Service
{
    public interface IPrefeituraService
    {
        int Create(Prefeitura prefeitura);
        void Edit(Prefeitura prefeitura);
        void Delete(int idPrefeitura);
        Prefeitura Get(int idPrefeitura);
        IEnumerable<Prefeitura> GetAll();
        IEnumerable<Prefeitura> GetByProfissional(int idCidadao);
        IEnumerable<PrefeituraCidadeDTO> GetAllCidade();
    }
}
