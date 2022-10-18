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
    public class CargoService : ICargoService
    {
        private readonly AgendeMeContext _context;

        public CargoService(AgendeMeContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Criar um novo cargo na base de dados
        /// </summary>
        /// <param name="cargo">dados do cargo</param>
        /// <returns>id do cargo</returns>
        public int Create(Cargo cargo)
        {
            _context.Add(cargo);
            _context.SaveChanges();
            return cargo.Id;
        }

        /// <summary>
        /// Remover o cargo da base de dados
        /// </summary>
        /// <param name="idCargo">id do cargo</param>
        public void Delete(int idCargo)
        {
            var _cargo = _context.Cargos.Find(idCargo);
            _context.Remove(_cargo);
            _context.SaveChanges();
        }

        /// <summary>
        /// Editar dados do cargo na base de dados
        /// </summary>
        /// <param name="cargo"></param
        public void Edit(Cargo cargo)
        {
            _context.Update(cargo);
            _context.SaveChanges();
        }

        /// <summary>
        /// Buscar um cargo na base de dados
        /// </summary>
        /// <param name="idCargo">id cargo</param>
        /// <returns>dados do cargo</returns>
        public Cargo Get(int idCargo)
        {
            return _context.Cargos.Find(idCargo);
        }

        /// <summary>
        /// Buscar todos os cargos cadastrados
        /// </summary>
        /// <returns>lista de cargos</returns>
        public IEnumerable<Cargo> GetAll()
        {
            return _context.Cargos.AsNoTracking();
        }

        public IEnumerable<Cargo> GetByProfissional(int idCidadao)
        {
            var query = (from cargos in _context.Cargos
                        from profissionalCargos in cargos.Profissionalcargos
                        where profissionalCargos.IdProfissional == idCidadao
                        select cargos);

            return query;
        }
    }
}
