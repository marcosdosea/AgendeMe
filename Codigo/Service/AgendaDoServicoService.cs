using AgendeMeWeb.Models;
using Core;
using Core.DTO;
using Core.Service;
using LinqKit;
using System.Linq.Expressions;

namespace Service
{
    public class AgendaDoServicoService : IAgendaDoServicoService
    {
        private readonly AgendeMeContext _context;

        public AgendaDoServicoService(AgendeMeContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Cria uma agenda do servico no banco de dados
        /// </summary>
        /// <param name="agendaDoServico">Dados da agenda do servico</param>
        /// <returns>Id da agenda do servico cadastrada</returns>
        public int Create(Agendadoservico agendaDoServico)
        {
            _context.Add(agendaDoServico);
            _context.SaveChanges();
            return agendaDoServico.Id;
        }
        /// <summary>
        /// Deleta uma agenda do servico no banco de dados
        /// </summary>
        /// <param name="id">Id da agenda do servico</param>
        public void Delete(int id)
        {
            var _agendaDoServico = _context.Agendamentos.Find(id);
            _context.Remove(_agendaDoServico);
            _context.SaveChanges();
        }
        /// <summary>
        /// Edita uma agenda do servico no banco de dados
        /// </summary>
        /// <param name="agendaDoServico">Dados da agenda do servico</param>
        public void Edit(Agendadoservico agendaDoServico)
        {
            _context.Update(agendaDoServico);
            _context.SaveChanges();
        }
        /// <summary>
        /// Consulta uma agenda do servico no banco de dados
        /// </summary>
        /// <param name="id">Id da agenda do servico</param>
        /// <returns>Dados da agenda do servico</returns>
        public Agendadoservico Get(int id)
        {
            return _context.Agendadoservicos.Find(id);
        }
        /// <summary>
        /// Consulta todas as agendas de servico
        /// </summary>
        /// <returns>Dados de todas as agendas de servico</returns>
        public IEnumerable<AgendadoservicoDTO> GetAll()
        {
            var query = from Agendadoservico in _context.Agendadoservicos
                        select new AgendadoservicoDTO
                        {
                            Id = Agendadoservico.Id,
                            DiaSemana = Agendadoservico.DiaSemana,
                            HorarioInicio = Agendadoservico.HorarioInicio,
                            HorarioFim = Agendadoservico.HorarioFim,
                            VagasAtendimento = Agendadoservico.VagasAtendimento,
                            VagasRetorno = Agendadoservico.VagasRetorno,
                            NomeDoServicoPublico = Agendadoservico.IdServicoPublicoNavigation.Nome,
                            NomeDoProfissional = Agendadoservico.IdProfissionalNavigation.Nome
                        };

            return query;
        }

        public IEnumerable<AgendadoservicoDTO> GetByPage(DatatableDTO model, out int filteredResultsCount, out int totalResultsCount)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;


            // if we have an empty search then just order the results by Id ascending
            var sortBy = "Id";
            var sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }
            var whereClause = BuildDynamicWhereClause(searchBy);

            var query = _context.Agendadoservicos
                   .Where(whereClause)
                   .Select(AgendaDoServico => new AgendadoservicoDTO
                   {
                       Id = AgendaDoServico.Id,
                       DiaSemana = AgendaDoServico.DiaSemana,
                       HorarioInicio = AgendaDoServico.HorarioInicio,
                       HorarioFim = AgendaDoServico.HorarioFim,
                       VagasAtendimento = AgendaDoServico.VagasAtendimento,
                       VagasRetorno = AgendaDoServico.VagasRetorno,
                       NomeDoProfissional = AgendaDoServico.IdProfissionalNavigation.Nome,
                       NomeDoServicoPublico = AgendaDoServico.IdServicoPublicoNavigation.Nome
                   })
                   //.OrderBy(sortBy, sortDir)
                   .Skip(skip)
                   .Take(take)
                   .ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            filteredResultsCount = _context.Agendadoservicos.Where(whereClause).Count();
            totalResultsCount = _context.Agendadoservicos.Count();

            return query;
        }

        private Expression<Func<Agendadoservico, bool>> BuildDynamicWhereClause(string? searchValue)
        {
            // simple method to dynamically plugin a where clause
            var predicate = PredicateBuilder.New<Agendadoservico>(true); // true -where(true) return all
            if (!String.IsNullOrWhiteSpace(searchValue))
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());

                predicate = predicate.Or(s => searchTerms.Any(srch => s.DiaSemana.ToLower().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.IdServicoPublicoNavigation.Nome.ToLower().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.IdProfissionalNavigation.Nome.ToLower().Contains(srch)));
            }
            return predicate;
        }
    }
}
