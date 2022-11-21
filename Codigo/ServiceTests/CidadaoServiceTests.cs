using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass()]
    public class CidadaoServiceTests
    {
        private AgendeMeContext _context;
        private ICidadaoService _cidadaoService;

        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            var builder = new DbContextOptionsBuilder<AgendeMeContext>();
            builder.UseInMemoryDatabase("AgendeMe");
            var options = builder.Options;

            _context = new AgendeMeContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var cidadao = new List<Cidadao>
                {
                    new Cidadao { Id = 1, Nome = "José Costa da Silva", Cpf = "121.510.775-78", Sexo = "M", DataNascimento = DateTime.Parse("1998-10-09"), Sus = "990 8504 6550 0000",
                                  Telefone = "(79) 98580-8281", Email = "heladio.alfradique@geradornv.com.br", TipoCidadao = "Cidadao",
                                  Estado = "SE", Cidade = "Ribeiropolis", Bairro = "Centro", Cep = "49530-000", Rua = "Rua 1", NumeroCasa = "45", Complemento = "Ao lado do hospital",
                                  IdOrgaoPublico = null, IdPrefeitura = null},

                    new Cidadao { Id = 2, Nome = "Maria Costa dos Santos", Sexo = "F", Cpf = "524.510.785-77",  DataNascimento = DateTime.Parse("1997-11-19"), Sus = "552 8504 6550 2543",
                                  Telefone = "(79) 98580-7852", Email = "maria.alfradique@geradornv.com.br", TipoCidadao = "Cidadao",
                                  Estado = "SE", Cidade = "Moita Bonita", Bairro = "Centro", Cep = "49560-000", Rua = "Rua B", NumeroCasa = "85", Complemento = "Ao lado da orla",
                                  IdOrgaoPublico = null, IdPrefeitura = null },

                    new Cidadao { Id = 3, Nome = "Marina Souza Santos", Cpf = "254.863.785-75", Sexo = "F", DataNascimento = DateTime.Parse("1999-01-29"), Sus = "892 8504 6550 2574",
                                  Telefone = "(79) 98580-7852", Email = "marina.12345@geradornv.com.br", TipoCidadao = "Cidadao",
                                  Estado = "SE", Cidade = "Moita Bonita", Bairro = "Centro", Cep = "49560-000", Rua = "Rua B", NumeroCasa = "85", Complemento = "Ao lado da escola",
                                  IdOrgaoPublico = null, IdPrefeitura = null},

                };

            _context.AddRange(cidadao);
            _context.SaveChanges();

            _cidadaoService = new CidadaoService(_context);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _cidadaoService.Create(new Cidadao()
            {
                Id = 4,
                Nome = "Marcos Oliveira Souza",
                Cpf = "475.863.785-88",
                Sexo = "M",
                DataNascimento = DateTime.Parse("1999-11-05"),
                Sus = "578 8504 6550 2365",
                Telefone = "(79) 98580-7743",
                Email = "marcosO.12345@geradornv.com.br",
                TipoCidadao = "Cidadao",
                Estado = "SE",
                Cidade = "Moita Bonita",
                Bairro = "Centro",
                Cep = "49560-000",
                Rua = "Rua A",
                NumeroCasa = "99",
                Complemento = "Ao lado da escola",
            });
            // Assert
            Assert.AreEqual(4, _cidadaoService.GetAll().Count());
            var cidadao = _cidadaoService.Get(4);
            Assert.AreEqual("Marcos Oliveira Souza", cidadao.Nome);
            Assert.AreEqual("475.863.785-88", cidadao.Cpf);
            Assert.AreEqual("M", cidadao.Sexo);
            Assert.AreEqual(DateTime.Parse("1999-11-05"), cidadao.DataNascimento);
            Assert.AreEqual("578 8504 6550 2365", cidadao.Sus);
            Assert.AreEqual("(79) 98580-7743", cidadao.Telefone);
            Assert.AreEqual("marcosO.12345@geradornv.com.br", cidadao.Email);
            Assert.AreEqual("Cidadao", cidadao.TipoCidadao);
            Assert.AreEqual("SE", cidadao.Estado);
            Assert.AreEqual("Moita Bonita", cidadao.Cidade);
            Assert.AreEqual("Centro", cidadao.Bairro);
            Assert.AreEqual("49560-000", cidadao.Cep);
            Assert.AreEqual("Rua A", cidadao.Rua);
            Assert.AreEqual("99", cidadao.NumeroCasa);
            Assert.AreEqual("Ao lado da escola", cidadao.Complemento);


        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            _cidadaoService.Delete(2);
            // Assert
            Assert.AreEqual(2, _cidadaoService.GetAll().Count());
            var cidadao = _cidadaoService.Get(2);
            Assert.AreEqual(null, cidadao);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Act 
            var cidadao = _cidadaoService.Get(3);
            cidadao.Nome = "Josefa Souza Santos";
            cidadao.Cpf = "778.863.785-75";
            cidadao.DataNascimento = DateTime.Parse("1995-01-18");
            cidadao.Telefone = "(79) 98580-7888";
            cidadao.Email = "josefa.12345@geradornv.com.br";
            cidadao.Cidade = "Itabaiana";
            cidadao.Bairro = "Centro";
            cidadao.Cep = "49000-000";
            cidadao.Rua = "Rua C";
            cidadao.NumeroCasa = "45";

            _cidadaoService.Edit(cidadao);
            //Assert
            cidadao = _cidadaoService.Get(3);
            Assert.IsNotNull(cidadao);
            Assert.AreEqual("Josefa Souza Santos", cidadao.Nome);
            Assert.AreEqual("778.863.785-75", cidadao.Cpf);
            Assert.AreEqual(DateTime.Parse("1995-01-18"), cidadao.DataNascimento);
            Assert.AreEqual("(79) 98580-7888", cidadao.Telefone);
            Assert.AreEqual("josefa.12345@geradornv.com.br", cidadao.Email);
            Assert.AreEqual("Itabaiana", cidadao.Cidade);
            Assert.AreEqual("Centro", cidadao.Bairro);
            Assert.AreEqual("49000-000", cidadao.Cep);
            Assert.AreEqual("Rua C", cidadao.Rua);
            Assert.AreEqual("45", cidadao.NumeroCasa);
        }

        [TestMethod()]
        public void GetTest()
        {
            var cidadao = _cidadaoService.Get(1);
            Assert.IsNotNull(cidadao);
            Assert.AreEqual("José Costa da Silva", cidadao.Nome);
            Assert.AreEqual("121.510.775-78", cidadao.Cpf);
            Assert.AreEqual("M", cidadao.Sexo);
            Assert.AreEqual("Ribeiropolis", cidadao.Cidade);
            Assert.AreEqual("Centro", cidadao.Bairro);
            Assert.AreEqual("49530-000", cidadao.Cep);
            Assert.AreEqual("Rua 1", cidadao.Rua);
            Assert.AreEqual("45", cidadao.NumeroCasa);

        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var listaCidadao = _cidadaoService.GetAll();
            // Assert
            Assert.IsInstanceOfType(listaCidadao, typeof(IEnumerable<Cidadao>));
            Assert.IsNotNull(listaCidadao);
            Assert.AreEqual(3, listaCidadao.Count());
            Assert.AreEqual(1, listaCidadao.First().Id);
            Assert.AreEqual("José Costa da Silva", listaCidadao.First().Nome);
        }
    }
}