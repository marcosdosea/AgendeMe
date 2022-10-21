using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class DiaAgendamentoService : IDiaAgendamentoService
    {
        private readonly AgendeMeContext _context;

        public DiaAgendamentoService(AgendeMeContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Cria um Dia para um agendamento
        /// </summary>
        /// <param name="diaAgendamento"></param>
        /// <returns></returns>
        public int Create(Diaagendamento diaAgendamento)
        {
            _context.Add(diaAgendamento);
            _context.SaveChanges();
            return diaAgendamento.Id;
        }
        /// <summary>
        /// Deleta um dia para um agendamento
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var _diaAgendamento = _context.Orgaopublicos.Find(id);
            _context.Remove(_diaAgendamento);
            _context.SaveChanges();
        }
        /// <summary>
        /// Edita um dia de agendamento
        /// </summary>
        /// <param name="diaAgendamento"></param>
        public void Edit(Diaagendamento diaAgendamento)
        {
            _context.Update(diaAgendamento);
            _context.SaveChanges();
        }
        /// <summary>
        /// Consulta um dia de agendamento por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Diaagendamento Get(int id)
        {
            return _context.Diaagendamentos.Find(id);
        }
        /// <summary>
        /// Consulta todos os dias agendamento
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Diaagendamento> GetAll()
        {
            return _context.Diaagendamentos.AsNoTracking();
        }
    }
}
