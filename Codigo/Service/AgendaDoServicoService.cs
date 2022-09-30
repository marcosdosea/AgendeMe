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
    public class AgendaDoServicoService : IAgendaDoServicoService
    {
        private readonly AgendeMeContext _context;

        public AgendaDoServicoService(AgendeMeContext context)
        {
            _context = context;
        }

        public int Create(Agendadoservico agendaDoServico)
        {
            _context.Add(agendaDoServico);
            _context.SaveChanges();
            return agendaDoServico.Id;
        }

        public void Delete(int id)
        {
            var _agendaDoServico = _context.Agendamentos.Find(id);
            _context.Remove(_agendaDoServico);
            _context.SaveChanges();
        }

        public void Edit(Agendadoservico agendaDoServico)
        {
            _context.Update(agendaDoServico);
            _context.SaveChanges();
        }

        public Agendadoservico Get(int id)
        {
            return _context.Agendadoservicos.Find(id);
        }

        public IEnumerable<Agendadoservico> GetAll()
        {
            return _context.Agendadoservicos.AsNoTracking();
        }
    }
}
