using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class PrefeituraService : IPrefeituraService
    {
        private readonly AgendeMeContext _context;

        public PrefeituraService(AgendeMeContext context)
        {
            _context = context;
        }

        /// <summary>
		/// Criar uma nova prefeitura na base de dados
		/// </summary>
		/// <param name="prefeitura">dados da prefeitura</param>
		/// <returns>id da prefeitura</returns>
        public int Create(Prefeitura prefeitura)
        {
            _context.Add(prefeitura);
            _context.SaveChanges();
            return prefeitura.Id;
        }

        /// <summary>
		/// Remover a prefeitura da base de dados
		/// </summary>
		/// <param name="idPrefeitura">id da prefeitura</param>
        public void Delete(int idPrefeitura)
        {
            var _prefeitura = _context.Prefeituras.Find(idPrefeitura);
            _context.Remove(_prefeitura);
            _context.SaveChanges();
        }

        /// <summary>
		/// Editar dados da prefeitura na base de dados
		/// </summary>
		/// <param name="prefeitura"></param>
        public void Edit(Prefeitura prefeitura)
        {
            _context.Update(prefeitura);
            _context.SaveChanges();
        }

        /// <summary>
		/// Buscar uma prefeitura na base de dados
		/// </summary>
		/// <param name="idPrefeitura">id prefeitura</param>
		/// <returns>dados da prefeitura</returns>
        public Prefeitura Get(int idPrefeitura)
        {
            return _context.Prefeituras.Find(idPrefeitura);
        }

        /// <summary>
		/// Buscar todos as prefeituras cadastradas
		/// </summary>
		/// <returns>lista de prefeituras</returns>
        public IEnumerable<Prefeitura> GetAll()
        {
            return _context.Prefeituras.AsNoTracking();
        }

        public IEnumerable<Prefeitura> GetByProfissional(int idCidadao)
        {
            /*var query = from prefeituras in _context.Prefeituras
                        from profissionalPrefeitura in prefeituras.Profissionalprefeituras
                        where profissionalPrefeitura.IdProfissional == idCidadao
                        select prefeituras;

            return query;
            */
            return null;
        }

        public IEnumerable<PrefeituraCidadeDTO> GetAllCidade()
        {
            var query = from prefeitura in _context.Prefeituras
                        select new PrefeituraCidadeDTO
                        {
                            Id = prefeitura.Id,
                            Cidade = prefeitura.Cidade,
                            Icone = prefeitura.Cidade.Substring(0, 2).ToUpper()
                        };

            return query;
        }
    }
}
