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
                    new Prefeitura { Id = 1, Nome = "Prefeitura Municipal de Moita Bonita", Cnpj = "13.104.112/0001-34", Estado = "SE", Cidade = "Moita Bonita",
                                             Bairro = "Centro", Cep = "49560-000", Rua = "Praça Santa Terezinha", Numero = "26", Icone = "BrasaoMoitaBonita.png" },

                    new Prefeitura { Id = 2, Nome = "Prefeitura Municipal de Ribeirópolis", Cnpj = "13.104.427/0001-81",  Estado = "SE", Cidade = "Ribeirópolis",
                                             Bairro = "Centro", Cep = "49530-000", Rua = "Avenida Barão do Rio Branco", Numero = "05", Icone = "BrasaoRibeiropolis.png" },

                    new Prefeitura { Id = 3, Nome = "Prefeitura Municipal de Itabaiana", Cnpj = "13.104.740/0001-10",  Estado = "SE", Cidade = "Itabaiana",
                                             Bairro = "Centro", Cep = "49500-970", Rua = "Praça Fausto Cardoso", Numero = "85", Icone = "BrasaoItabaiana.png"},

                };

            _context.AddRange(prefeitura);
            _context.SaveChanges();

            _prefeituraService = new PrefeituraService(_context);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _prefeituraService.Create(new Prefeitura()
            {
                Id = 4,
                Nome = "Prefeitura Municipal de Salvador",
                Cnpj = "13.927.801/0001-49",
                Estado = "BA",
                Cidade = "Salvador",
                Bairro = "Baixa Norte",
                Cep = "40020-010",
                Rua = "Praça Municipal -  Pálacio Thomé de Souza",
                Numero = "S/N",
                Icone = "BrasaoSalvadorBahia.png"
            });
            // Assert
            Assert.AreEqual(4, _prefeituraService.GetAll().Count());
            var prefeitura = _prefeituraService.Get(4);
            Assert.AreEqual("Prefeitura Municipal de Salvador", prefeitura.Nome);
            Assert.AreEqual("13.927.801/0001-49", prefeitura.Cnpj);
            Assert.AreEqual("BA", prefeitura.Estado);
            Assert.AreEqual("Salvador", prefeitura.Cidade);
            Assert.AreEqual("Baixa Norte", prefeitura.Bairro);
            Assert.AreEqual("40020-010", prefeitura.Cep);
            Assert.AreEqual("Praça Municipal -  Pálacio Thomé de Souza", prefeitura.Rua);
            Assert.AreEqual("S/N", prefeitura.Numero);
            Assert.AreEqual("BrasaoSalvadorBahia.png", prefeitura.Icone);
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
            prefeitura.Nome = "Prefeitura Municipal de São Paulo";
            prefeitura.Cnpj = "49.269.236/0019-46";
            prefeitura.Estado = "SP";
            prefeitura.Cidade = "São Paulo";
            prefeitura.Bairro = "Centro";
            prefeitura.Cep = "01002-900";
            prefeitura.Rua = "Viaduto do Chá";
            prefeitura.Numero = "15";
            prefeitura.Icone = "brasaoPrefeituraSaoPaulo.png";

            _prefeituraService.Edit(prefeitura);
            //Assert
            prefeitura = _prefeituraService.Get(3);
            Assert.IsNotNull(prefeitura);
            Assert.AreEqual("Prefeitura Municipal de São Paulo", prefeitura.Nome);
            Assert.AreEqual("49.269.236/0019-46", prefeitura.Cnpj);
            Assert.AreEqual("SP", prefeitura.Estado);
            Assert.AreEqual("São Paulo", prefeitura.Cidade);
            Assert.AreEqual("Centro", prefeitura.Bairro);
            Assert.AreEqual("01002-900", prefeitura.Cep);
            Assert.AreEqual("Viaduto do Chá", prefeitura.Rua);
            Assert.AreEqual("15", prefeitura.Numero);
            Assert.AreEqual("brasaoPrefeituraSaoPaulo.png", prefeitura.Icone);
        }

        [TestMethod()]
        public void GetTest()
        {
            var prefeitura = _prefeituraService.Get(1);
            Assert.IsNotNull(prefeitura);
            Assert.AreEqual("Prefeitura Municipal de Moita Bonita", prefeitura.Nome);
            Assert.AreEqual("13.104.112/0001-34", prefeitura.Cnpj);
            Assert.AreEqual("SE", prefeitura.Estado);
            Assert.AreEqual("Moita Bonita", prefeitura.Cidade);
            Assert.AreEqual("Centro", prefeitura.Bairro);
            Assert.AreEqual("49560-000", prefeitura.Cep);
            Assert.AreEqual("Praça Santa Terezinha", prefeitura.Rua);
            Assert.AreEqual("26", prefeitura.Numero);
            Assert.AreEqual("BrasaoMoitaBonita.png", prefeitura.Icone);

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
            Assert.AreEqual("Prefeitura Municipal de Moita Bonita", listaPrefeitura.First().Nome);
        }
    }
}