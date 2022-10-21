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

        public void DeletProfissional(int idCidadao, int idCargo, int idPrefeitura)
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

        public IEnumerable<ProfissionalDTO> GetProfissional(int id)
        {
            var profissional = (from cidadao in _context.Cidadaos
                                where cidadao.Id == id
                                from prefeituras in cidadao.Profissionalprefeituras
                                from cargos in cidadao.Profissionalcargos
                                select new ProfissionalDTO
                                {
                                    NomeCidadao = cidadao.Nome,
                                    IdCidadao = cidadao.Id,
                                    NomeCargo = cargos.IdCargoNavigation.Nome,
                                    NomePrefeitura = prefeituras.IdPrefeituraNavigation.Nome
                                });

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
