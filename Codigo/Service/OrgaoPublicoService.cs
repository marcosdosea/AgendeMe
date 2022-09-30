using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var _orgaoPublico = _context.Orgaopublicos.Find(id);
            _context.Remove(_orgaoPublico);
            _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgaoPublico"></param>
        public void Edit(Orgaopublico orgaoPublico)
        {
            _context.Update(orgaoPublico);
            _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Orgaopublico Get(int id)
        {
            return _context.Orgaopublicos.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Orgaopublico> GetAll()
        {
            return _context.Orgaopublicos.AsNoTracking();
        }
    }
}
