using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class OrgaoPublicoService : IOrgaoPublicoService
    {
        private readonly AgendeMeContext _context;

        public OrgaoPublicoService(AgendeMeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Insere um novo órgão público na base de dados 
        /// </summary>
        /// <param name="orgaoPublico"></param>
        /// <returns></returns>
        public int Create(Orgaopublico orgaoPublico)
        {
            _context.Add(orgaoPublico);
            _context.SaveChanges();
            return orgaoPublico.Id;
        }
        /// <summary>
        /// Deleta um órgão público presente na base de dados
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var _orgaoPublico = _context.Orgaopublicos.Find(id);
            _context.Remove(_orgaoPublico);
            _context.SaveChanges();
        }

        /// <summary>
        /// Edita um órgão público presente na base de dados
        /// </summary>
        /// <param name="orgaoPublico"></param>
        public void Edit(Orgaopublico orgaoPublico)
        {
            _context.Update(orgaoPublico);
            _context.SaveChanges();
        }

        /// <summary>
        /// Busca um órgão público na base de dados 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Orgaopublico Get(int id)
        {
            return _context.Orgaopublicos.Find(id);
        }

        /// <summary>
        /// Busca todos os órgãos públicos presentes na base de dados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Orgaopublico> GetAll()
        {
            return _context.Orgaopublicos.AsNoTracking();
        }
        /// <summary>
        /// Consulta todos os orgaos publicos que oferecem um determinado servico
        /// </summary>
        /// <param name="nome">nome do servico publico</param>
        /// <returns>Todos os orgaos publicos</returns>
        public IEnumerable<Orgaopublico> GetAllByNomeServicoPublico(string nome)
        {
            var query = from Servicopublico in _context.Servicopublicos
                        where Servicopublico.Nome.Equals(nome)
                        select Servicopublico.IdOrgaoPublicoNavigation;

            return query.AsNoTracking();
        }
    }
}
