namespace Core.Service
{
    public interface ICargoService
    {
        int Create(Cargo cargo);
        void Edit(Cargo cargo);
        void Delete(int idCargo);
        Cargo Get(int idCargo);
        IEnumerable<Cargo> GetByProfissional(int idCidadao);

        IEnumerable<Cargo> GetAll();
    }
}
