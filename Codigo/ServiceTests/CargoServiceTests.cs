using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass()]
    public class CargoServiceTests
    {
        private AgendeMeContext _context;
        private ICargoService _cargoService;

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
            var cargos = new List<Cargo>
                {
                    new Cargo { Id = 1, Nome = "Gestor de Orgão", Descricao = "Responsável por um determinado órgão público" },
                    new Cargo { Id = 2, Nome = "Atendente", Descricao = "Responsável por atender os usuários em um orgão e mudar a situação dos agendamentos" },
                    new Cargo { Id = 3, Nome = "Clínico Geral", Descricao = "Médico responsável pela primeira avaliação de um paciente" },
                };

            _context.AddRange(cargos);
            _context.SaveChanges();

            _cargoService = new CargoService(_context);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _cargoService.Create(new Cargo() { Id = 4, Nome = "Eletricista", Descricao = "Responsável por cuidar de problemas com redes elétricas nos órgãos" });
            // Assert
            Assert.AreEqual(4, _cargoService.GetAll().Count());
            var cargo = _cargoService.Get(4);
            Assert.AreEqual("Eletricista", cargo.Nome);
            Assert.AreEqual("Responsável por cuidar de problemas com redes elétricas nos órgãos", cargo.Descricao);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            _cargoService.Delete(2);
            // Assert
            Assert.AreEqual(2, _cargoService.GetAll().Count());
            var cargo = _cargoService.Get(2);
            Assert.AreEqual(null, cargo);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Act 
            var cargo = _cargoService.Get(3);
            cargo.Nome = "Tecnico em Informatica";
            cargo.Descricao = "Responsável por manutenção de todos os dispostivos eletrônicos dos órgãos";
            _cargoService.Edit(cargo);
            //Assert
            cargo = _cargoService.Get(3);
            Assert.IsNotNull(cargo);
            Assert.AreEqual("Tecnico em Informatica", cargo.Nome);
            Assert.AreEqual("Responsável por manutenção de todos os dispostivos eletrônicos dos órgãos", cargo.Descricao);
        }

        [TestMethod()]
        public void GetTest()
        {
            var cargo = _cargoService.Get(1);
            Assert.IsNotNull(cargo);
            Assert.AreEqual("Gestor de Orgão", cargo.Nome);
            Assert.AreEqual("Responsável por um determinado órgão público", cargo.Descricao);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var listaCargo = _cargoService.GetAll();
            // Assert
            Assert.IsInstanceOfType(listaCargo, typeof(IEnumerable<Cargo>));
            Assert.IsNotNull(listaCargo);
            Assert.AreEqual(3, listaCargo.Count());
            Assert.AreEqual(1, listaCargo.First().Id);
            Assert.AreEqual("Gestor de Orgão", listaCargo.First().Nome);
        }
    }
}