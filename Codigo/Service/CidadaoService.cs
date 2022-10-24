using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;

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
            Cargoprofissionalprefeitura profissional = new()
            {
                IdCargo = idCargo,
                IdPrefeitura = idPrefeitura,
                IdProfissional = idCidadao
            };

            _context.Add(profissional);
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

        public void DeleteProfissional(int idCidadao, string nomeCargo, string nomePrefeitura)
        {

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

        /// <summary>
        /// Editar cargo de um profissional existente
        /// </summary>
        /// <param name="cidadao"></param>
        /// <param name="prefeitura">prefeitura</param>
        /// <param name="cargo">cargo</param>
        /// <returns></returns>
        public void EditProfissional(int idCidadao, string nomePrefeitura, string nomeCargo)
        {
            int idCargo = int.Parse((from cargos in _context.Cargos
                                     where cargos.Nome == nomeCargo
                                     select cargos.Id).First().ToString());

            int idPrefeitura = int.Parse((from prefeitura in _context.Cargos
                                          where prefeitura.Nome == nomePrefeitura
                                          select prefeitura.Id).First().ToString());

            Cargoprofissionalprefeitura? cargoprofissionalprefeitura = _context.Cargoprofissionalprefeituras.SingleOrDefault(p => p.IdPrefeitura == idPrefeitura
                                                                                                                               && p.IdCargo == idCargo);
            cargoprofissionalprefeitura.IdCargo = idCargo;

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
            var query = from cidadao in _context.Cidadaos
                        from profissional in cidadao.Cargoprofissionalprefeituras
                        where profissional.IdPrefeituraNavigation.Nome == nomePrefeitura
                        where profissional.IdCargoNavigation.Nome == nomeCargo
                        select new ProfissionalDTO
                        {
                            IdCidadao = cidadao.Id,
                            NomeCidadao = cidadao.Nome,
                            NomeCargo = profissional.IdCargoNavigation.Nome,
                            NomePrefeitura = profissional.IdPrefeituraNavigation.Nome
                        };
            return query.First();
        }

        /// <summary>
        /// Buscar todos os cidadãos cadastrados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cidadao> GetAll()
        {
            return _context.Cidadaos.AsNoTracking();
        }

        public IEnumerable<ProfissionalDTO> GetAllProfissional()
        {
            var query = from cidadao in _context.Cidadaos
                        from profissional in cidadao.Cargoprofissionalprefeituras
                        where profissional.IdPrefeitura != null
                        select new ProfissionalDTO
                        {
                            IdCidadao = cidadao.Id,
                            NomeCidadao = cidadao.Nome,
                            NomeCargo = profissional.IdCargoNavigation.Nome,
                            NomePrefeitura = profissional.IdPrefeituraNavigation.Nome
                        };
            return query.AsNoTracking();
        }

        public IEnumerable<CidadaoDTO> GetById(int idCidadao)
        {
            var query = from cidadao in _context.Cidadaos
                        where cidadao.Id == idCidadao
                        select new CidadaoDTO
                        {
                            Id = cidadao.Id,
                            Nome = cidadao.Nome,
                            Cpf = cidadao.Cpf,
                            DataNascimento = cidadao.DataNascimento,
                            Sexo = cidadao.Sexo,
                            Sus = cidadao.Sus,
                            Telefone = cidadao.Telefone,
                            Email = cidadao.Email,
                            Cep = cidadao.Cep,
                            Estado = cidadao.Estado,
                            Cidade = cidadao.Cidade,
                            Bairro = cidadao.Bairro,
                            Rua = cidadao.Rua,
                            NumeroCasa = cidadao.NumeroCasa,
                            Complemento = cidadao.Complemento,
                        };
            return query.AsNoTracking();
        }
    }
}
