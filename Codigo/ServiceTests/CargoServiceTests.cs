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
                    new Cargo { Id = 1, Nome = "Motorista", Descricao = "Dirige os veículos" },
                    new Cargo { Id = 2, Nome = "Personal trainer", Descricao = "Professor de educação física" },
                    new Cargo { Id = 3, Nome = "Marcos Dósea", Descricao = "PO" },
                };

            _context.AddRange(cargos);
            _context.SaveChanges();

            _cargoService = new CargoService(_context);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _cargoService.Create(new Cargo() { Id = 4, Nome = "Eletricista", Descricao = "mexe com a rede eletrica" });
            // Assert
            Assert.AreEqual(4, _cargoService.GetAll().Count());
            var cargo = _cargoService.Get(4);
            Assert.AreEqual("Eletricista", cargo.Nome);
            Assert.AreEqual("mexe com a rede eletrica", cargo.Descricao);
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
            cargo.Nome = "Tecnico em informatica";
            cargo.Descricao = "Conserta impressora";
            _cargoService.Edit(cargo);
            //Assert
            cargo = _cargoService.Get(3);
            Assert.IsNotNull(cargo);
            Assert.AreEqual("Tecnico em informatica", cargo.Nome);
            Assert.AreEqual("Conserta impressora", cargo.Descricao);
        }

        [TestMethod()]
        public void GetTest()
        {
            var cargo = _cargoService.Get(1);
            Assert.IsNotNull(cargo);
            Assert.AreEqual("Motorista", cargo.Nome);
            Assert.AreEqual("Dirige os veículos", cargo.Descricao);
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
            Assert.AreEqual("Motorista", listaCargo.First().Nome);
        }
    }
}