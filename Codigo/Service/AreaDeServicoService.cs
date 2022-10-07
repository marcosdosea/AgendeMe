using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class AreaDeServicoService : IAreaDeServicoService
    {
        private readonly AgendeMeContext _context;

        public AreaDeServicoService(AgendeMeContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Cria uma area de servico no banco de dados
        /// </summary>
        /// <param name="areaDeServico">Dados da area de servico</param>
        /// <returns>Id da area de servico criada</returns>
        public int Create(Areadeservico areaDeServico)
        {
            _context.Add(areaDeServico);
            _context.SaveChanges();
            return areaDeServico.Id;
        }
        /// <summary>
        /// Deleta uma area de servico do banco de dados
        /// </summary>
        /// <param name="idAreaDeServico">Id da area de servico</param>
        public void Delete(int idAreaDeServico)
        {
            var _areaDeServico = _context.Agendamentos.Find(idAreaDeServico);
            _context.Remove(_areaDeServico);
            _context.SaveChanges();
        }
        /// <summary>
        /// Edita uma area de servico do banco de dados
        /// </summary>
        /// <param name="areaDeServico">Dados da area de servico</param>
        public void Edit(Areadeservico areaDeServico)
        {
            _context.Update(areaDeServico);
            _context.SaveChanges();
        }
        /// <summary>
        /// Consulta uma area de servico do banco de dados
        /// </summary>
        /// <param name="idAreaDeServico">Id da area de servico</param>
        /// <returns></returns>
        public Areadeservico Get(int idAreaDeServico)
        {
            return _context.Areadeservicos.Find(idAreaDeServico);
        }
        /// <summary>
        /// Consulta todas as areas de servico do banco de dados
        /// </summary>
        /// <returns>Todas as areas de servico</returns>
        public IEnumerable<Areadeservico> GetAll()
        {
            return _context.Areadeservicos.AsNoTracking();
        }
        /// <summary>
        /// Consulta todas as areas de servico a partir de um nome de prefeitura
        /// </summary>
        /// <param name="NomePrefeitura">Nome da prefeitura</param>
        /// <returns>Todas as areas de servico da prefeitura</returns>
        public IEnumerable<Areadeservico> GetAllByNomePrefeitura(String NomePrefeitura)
        {
            var query = from Areadeservico in _context.Areadeservicos
                        join Prefeitura in _context.Prefeituras
                        on Areadeservico.IdPrefeitura equals Prefeitura.Id
                        where Prefeitura.Nome == "Prefeitura de " + NomePrefeitura
                        select Areadeservico;

            return query;
        }
    }
}
