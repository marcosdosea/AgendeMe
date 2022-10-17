namespace Core.Service
{
    public interface IAreaDeServicoService
    {
        int Create(Areadeservico areaDeServico);
        void Edit(Areadeservico areaDeServico);
        void Delete(int idAreaDeServico);
        Areadeservico Get(int idAreaDeServico);
        IEnumerable<Areadeservico> GetAll();
        IEnumerable<Areadeservico> GetAllByIdPrefeitura(int id);
    }
}
