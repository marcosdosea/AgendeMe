using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class ServicoPublicoService : IServicoPublicoService
    {
        private readonly AgendeMeContext _context;

        public ServicoPublicoService(AgendeMeContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Cria um novo servico publico no banco de dados
        /// </summary>
        /// <param name="servicoPublico">Dados do serico publico</param>
        /// <returns>Id do servico publico criado</returns>
        public int Create(Servicopublico servicoPublico)
        {
            _context.Add(servicoPublico);
            _context.SaveChanges();
            return servicoPublico.Id;
        }
        /// <summary>
        /// Deleta um servico publico no banco de dados
        /// </summary>
        /// <param name="id">Id do servico publico</param>
        public void Delete(int id)
        {
            var _servicoPublico = _context.Servicopublicos.Find(id);
            _context.Remove(_servicoPublico);
            _context.SaveChanges();
        }
        /// <summary>
        /// Edita um servico publico no banco de dados
        /// </summary>
        /// <param name="servicoPublico"></param>
        public void Edit(Servicopublico servicoPublico)
        {
            _context.Update(servicoPublico);
            _context.SaveChanges();
        }
        /// <summary>
        /// Consulta um servico publico no banco de dados
        /// </summary>
        /// <param name="id">Id do servico publico</param>
        /// <returns>Dados do servico publico</returns>
        public Servicopublico Get(int id)
        {
            return _context.Servicopublicos.Find(id);
        }
        /// <summary>
        /// Consulta todos os servicos publicos no banco de dados
        /// </summary>
        /// <returns>Dados de todos os servicos publicos</returns>
        public IEnumerable<Servicopublico> GetAll()
        {
            return _context.Servicopublicos.AsNoTracking();
        }
        /// <summary>
        /// Consulta todos os servicos publicos a partir do id de uma area de servico
        /// </summary>
        /// <param name="idArea">Id da area de servico</param>
        /// <returns>Servicos publicos para uma area de servico</returns>
        public IEnumerable<ServicoPublicoDTO> GetAllByIdArea(int idArea)
        {
            var query = from servicoPublico in _context.Servicopublicos
                        where servicoPublico.IdArea.Equals(idArea)
                        group servicoPublico by new
                        {
                            servicoPublico.Nome,
                            servicoPublico.Icone
                        } into servicoPublicoGroup
                        orderby servicoPublicoGroup.Key.Nome
                        select new ServicoPublicoDTO
                        {
                            Nome = servicoPublicoGroup.Key.Nome,
                            Icone = servicoPublicoGroup.Key.Icone
                        };

            return query.AsNoTracking();
        }

        public IEnumerable<Servicopublico> GetAllByIdOrgao(int idOrgao)
        {
            var query = _context.Servicopublicos.Where(s => s.IdOrgaoPublico == idOrgao);
            return query.AsNoTracking();
        }
    }
}
