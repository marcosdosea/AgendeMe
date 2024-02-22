using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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
        public int AddProfissional(int IdProfissional, int idPrefeitura, int idCargo)
        {
            Cargoprofissionalprefeitura profissional = new()
            {
                IdCargo = idCargo,
                IdPrefeitura = idPrefeitura,
                IdProfissional = IdProfissional
            };

            _context.Add(profissional);
            _context.SaveChanges();

            return IdProfissional;

        }

        /// <summary>
        /// Inserir um novo cidadão na base de dados
        /// </summary>
        /// <param name="cidadao"></param>
        /// <returns></returns>
        public int Create(Cidadao cidadao)
        {
            if (cidadao.TipoCidadao == null)
                cidadao.TipoCidadao = "Cidadao";

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

        public void DeleteProfissional(int IdCargo, int IdProfissional, int IdPrefeitura)
        {
            var _profissional = _context.Cargoprofissionalprefeituras.Find(IdCargo, IdProfissional, IdPrefeitura);
            _context.Remove(_profissional);
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

        /// <summary>
        /// Editar cargo de um profissional existente
        /// </summary>
        /// <param name="cidadao"></param>
        /// <param name="prefeitura">prefeitura</param>
        /// <param name="cargo">cargo</param>
        /// <returns></returns>
        public void EditProfissional(Cargoprofissionalprefeitura profissional)
        {

            _context.Update(profissional);

            _context.SaveChanges();
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

        public Cargoprofissionalprefeitura GetProfissional(int IdProfissional, int IdCargo, int IdPrefeitura)
        {
            return _context.Cargoprofissionalprefeituras.Find(IdCargo, IdProfissional, IdPrefeitura);
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
                        orderby cidadao.Nome
                        select new ProfissionalDTO
                        {
                            IdProfissional = cidadao.Id,
                            IdPrefeitura = profissional.IdPrefeituraNavigation.Id,
                            IdCargo = profissional.IdCargoNavigation.Id,
                            NomeProfissional = cidadao.Nome,
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

        public CidadaoDTO? GetByCPF(string CPF)
        {
            var query = from cidadao in _context.Cidadaos
                        where cidadao.Cpf == CPF
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
                            IdPrefeitura = cidadao.IdPrefeitura,
                            Prefeitura = _context.Prefeituras.FirstOrDefault(p => p.Cidade.Equals(cidadao.Cidade)),
                            Papel = cidadao.TipoCidadao,
                            IdOrgao = cidadao.IdOrgaoPublicoNavigation.Id
                        };
            
            if (query.Any())
                return query.AsNoTracking().First();
            return null;
        }

        public CidadaoDTO? GetByEmail(string email)
        {
            var query = from cidadao in _context.Cidadaos
                        where cidadao.Email == email
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
                            IdPrefeitura = cidadao.IdPrefeitura,
                            Prefeitura = _context.Prefeituras.FirstOrDefault(p => p.Cidade.Equals(cidadao.Cidade)),
                            Papel = cidadao.TipoCidadao
                        };
            if (query.Any())
                return query.AsNoTracking().First();
            return null;
        }

        public async Task<bool> AddCidadaoAsync(UsuarioIdentity user, 
            IUserStore<UsuarioIdentity> userStore, 
            UserManager<UsuarioIdentity> userManager, 
            IUserEmailStore<UsuarioIdentity> emailStore,
            Cidadao cidadao, 
            string senha) 
        {
            try 
            {
                await userStore.SetUserNameAsync(user, cidadao.Cpf, CancellationToken.None);
                await emailStore.SetEmailAsync(user, cidadao.Email, CancellationToken.None);
                var result = await userManager.CreateAsync(user, senha);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "CIDADAO");
                    Create(cidadao);
                    return true;
                } 
                return false;
            } 
            catch 
            {
                await userManager.DeleteAsync(user);
                return false;
            }
        }
        
    }
}
