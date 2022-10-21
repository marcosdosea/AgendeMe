using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass()]
    public class PrefeituraServiceTests
    {
        private AgendeMeContext _context;
        private IPrefeituraService _prefeituraService;

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
            var prefeitura = new List<Prefeitura>
                {
                    new Prefeitura { Id = 1, Nome = "Prefeitura de Motorista", Cnpj = "94.259.898/0001-60", Estado = "SE", Cidade = "Ribeiropolis",
                                             Bairro = "Centro", Cep = "49530-000", Rua = "Rua 1", Numero = "45", Icone = "dsfse" },

                    new Prefeitura { Id = 2, Nome = "Prefeitura de Personal trainer", Cnpj = "90.796.680/0001-94",  Estado = "SE", Cidade = "Ribeiropolis",
                                             Bairro = "Centro", Cep = "49530-000", Rua = "Rua 2", Numero = "65", Icone = "dsfse" },

                    new Prefeitura { Id = 3, Nome = "Prefeitura de Marcos Dósea", Cnpj = "71.375.863/0001-91",  Estado = "SE", Cidade = "Ribeiropolis",
                                             Bairro = "Centro", Cep = "49530-000", Rua = "Rua 3", Numero = "85", Icone = "dsfse"},

                };

            _context.AddRange(prefeitura);
            _context.SaveChanges();

            _prefeituraService = new PrefeituraService(_context);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _prefeituraService.Create(new Prefeitura() { Id = 4, Nome = "Prefeitura de Eletricista", Cnpj = "71.375.863/0001-00", Estado = "BA", Cidade = "Salvador",
                                                                 Bairro = "Baixa Norte", Cep = "49530-699", Rua = "Rua 4", Numero = "105", Icone = "HTTPS2168" });
            // Assert
            Assert.AreEqual(4, _prefeituraService.GetAll().Count());
            var prefeitura = _prefeituraService.Get(4);
            Assert.AreEqual("Prefeitura de Eletricista", prefeitura.Nome);
            Assert.AreEqual("71.375.863/0001-00", prefeitura.Cnpj);
            Assert.AreEqual("BA", prefeitura.Estado);
            Assert.AreEqual("Salvador", prefeitura.Cidade);
            Assert.AreEqual("Baixa Norte", prefeitura.Bairro);
            Assert.AreEqual("49530-699", prefeitura.Cep);
            Assert.AreEqual("Rua 4", prefeitura.Rua);
            Assert.AreEqual("105", prefeitura.Numero);
            Assert.AreEqual("HTTPS2168", prefeitura.Icone);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            _prefeituraService.Delete(2);
            // Assert
            Assert.AreEqual(2, _prefeituraService.GetAll().Count());
            var prefeitura = _prefeituraService.Get(2);
            Assert.AreEqual(null, prefeitura);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Act 
            var prefeitura = _prefeituraService.Get(3);
            prefeitura.Nome = "Prefeitura de informatica";
            prefeitura.Cnpj = "79.520.261/0001-92";
            prefeitura.Estado = "SP";
            prefeitura.Cidade = "São Paulo";
            prefeitura.Bairro = "Centro";
            prefeitura.Cep = "00000-000";
            prefeitura.Rua = "Dois";
            prefeitura.Numero = "765";
            prefeitura.Icone = "dasdew";

            _prefeituraService.Edit(prefeitura);
            //Assert
            prefeitura = _prefeituraService.Get(3);
            Assert.IsNotNull(prefeitura);
            Assert.AreEqual("Prefeitura de informatica", prefeitura.Nome);
            Assert.AreEqual("79.520.261/0001-92", prefeitura.Cnpj);
            Assert.AreEqual("SP", prefeitura.Estado);
            Assert.AreEqual("São Paulo", prefeitura.Cidade);
            Assert.AreEqual("Centro", prefeitura.Bairro);
            Assert.AreEqual("00000-000", prefeitura.Cep);
            Assert.AreEqual("Dois", prefeitura.Rua);
            Assert.AreEqual("765", prefeitura.Numero);
            Assert.AreEqual("dasdew", prefeitura.Icone);
        }

        [TestMethod()]
        public void GetTest()
        {
            var prefeitura = _prefeituraService.Get(1);
            Assert.IsNotNull(prefeitura);
            Assert.AreEqual("Prefeitura de Motorista", prefeitura.Nome);
            Assert.AreEqual("94.259.898/0001-60", prefeitura.Cnpj);
            Assert.AreEqual("SE", prefeitura.Estado);
            Assert.AreEqual("Ribeiropolis", prefeitura.Cidade);
            Assert.AreEqual("Centro", prefeitura.Bairro);
            Assert.AreEqual("49530-000", prefeitura.Cep);
            Assert.AreEqual("Rua 1", prefeitura.Rua);
            Assert.AreEqual("45", prefeitura.Numero);
            Assert.AreEqual("dsfse", prefeitura.Icone);

        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var listaPrefeitura = _prefeituraService.GetAll();
            // Assert
            Assert.IsInstanceOfType(listaPrefeitura, typeof(IEnumerable<Prefeitura>));
            Assert.IsNotNull(listaPrefeitura);
            Assert.AreEqual(3, listaPrefeitura.Count());
            Assert.AreEqual(1, listaPrefeitura.First().Id);
            Assert.AreEqual("Prefeitura de Motorista", listaPrefeitura.First().Nome);
        }
    }
}