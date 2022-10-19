using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CidadaoService : ICidadaoService
    {
        private readonly AgendeMeContext _context;

        public CidadaoService(AgendeMeContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adicionar um cidadão existente como profissional (associa a um cargo e a uma prefeitura)
        /// </summary>
        /// <param name="cidadao">cidadao</param>
        /// <param name="prefeitura">prefeitura</param>
        /// <param name="cargo">cargo</param>
        /// <returns></returns>
        public int AddProfissional(int idCidadao, int idPrefeitura, int idCargo)
        {
            Profissionalcargo profissionalcargo = new();
            profissionalcargo.IdCargo = idCargo;
            profissionalcargo.IdProfissional = idCidadao;

            Profissionalprefeitura profissionalprefeitura = new();
            profissionalprefeitura.IdProfissional = idCidadao;
            profissionalprefeitura.IdPrefeitura = idPrefeitura;

            _context.Add(profissionalcargo);
            _context.SaveChanges();

            _context.Add(profissionalprefeitura);
            _context.SaveChanges();

            return idCidadao;

        }

        /// <summary>
        /// Inserir um novo cidadão na base de dados
        /// </summary>
        /// <param name="cidadao"></param>
        /// <returns></returns>
        public int Create(Cidadao cidadao)
        {
            _context.Add(cidadao);
            _context.SaveChanges();
            return cidadao.Id;
        }

        /// <summary>
        /// Deletar um cidadão da base de dados
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var _cidadao = _context.Cidadaos.Find(id);
            _context.Remove(_cidadao);
            _context.SaveChanges();
        }

        public void DeleteProfissional(int idCidadao, int idCargo, int idPrefeitura)
        {
            var _profissionalCargo = _context.Profissionalcargos.Find(idCargo, idCidadao);
            var _profissionalPrefeitura = _context.Profissionalprefeituras.Find(idCidadao, idPrefeitura);

            _context.Remove(_profissionalCargo);
            _context.SaveChanges();

            _context.Remove(_profissionalPrefeitura);
            _context.SaveChanges();
        }

        /// <summary>
        /// Editar dados de um cidadão cadastrado na base de dados
        /// </summary>
        /// <param name="cidadao"></param>
        public void Edit(Cidadao cidadao)
        {
            _context.Update(cidadao);
            _context.SaveChanges();
        }

        public void EditProfissional()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Buscar um cidadão cadastrado na base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Cidadao Get(int id)
        {
            return _context.Cidadaos.Find(id);
        }

        public ProfissionalDTO GetProfissional(int idCidadao, string nomeCargo, string nomePrefeitura)
        {
            var profissional = (from cidadao in _context.Cidadaos
                                            where cidadao.Id == idCidadao
                                            from cargos in cidadao.Profissionalcargos.Where(p => p.IdCargoNavigation.Nome == nomeCargo)
                                            from prefeituras in cidadao.Profissionalprefeituras.Where(p => p.IdPrefeituraNavigation.Nome == nomePrefeitura)
                                            select new ProfissionalDTO
                                            {
                                                NomeCidadao = cidadao.Nome,
                                                IdCidadao = cidadao.Id,
                                                NomeCargo = cargos.IdCargoNavigation.Nome,
                                                NomePrefeitura = prefeituras.IdPrefeituraNavigation.Nome
                                            }).FirstOrDefault() ;

            return profissional;
        }

        /// <summary>
        /// Buscar todos os cidadãos cadastrados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cidadao> GetAll()
        {
            return _context.Cidadaos.AsNoTracking();
        }

        public IEnumerable<ProfissionalDTO> GetAllProfissional(int idPrefeitura)
        {

            var query = from cidadao in _context.Cidadaos
                        from prefeituras in cidadao.Profissionalprefeituras.Where(p => p.IdPrefeitura > 0)
                        from cargos in cidadao.Profissionalcargos.Where(p => p.IdCargo > 0)
                        orderby cidadao.Nome
                        select new ProfissionalDTO
                        {
                            NomeCidadao = cidadao.Nome,
                            IdCidadao = cidadao.Id,
                            NomeCargo = cargos.IdCargoNavigation.Nome,
                            NomePrefeitura = prefeituras.IdPrefeituraNavigation.Nome
                        };

            return query;
        }
    }
}
