using Core.DTO;

namespace Core.Service
{
    public interface IOrgaoPublicoService
    {
        int Create(Orgaopublico orgaoPublico);
        void Edit(Orgaopublico orgaoPublico);
        void Delete(int id);
        Orgaopublico Get(int id);
        IEnumerable<Orgaopublico> GetAll();
        IEnumerable<OrgaoPublicoDTO> GetAllByNomeServicoPublico(string nomeServico);
    }
}
