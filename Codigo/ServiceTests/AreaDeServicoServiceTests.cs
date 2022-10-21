using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tests
{
    [TestClass()]
    public class AreaDeServicoServiceTests
    {
        private AgendeMeContext _context;
        private IAreaDeServicoService _areaDeServicoService;

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
            var areasDeServico = new List<Areadeservico>
            {
                new Areadeservico {Id = 1, Nome = "Saúde", IdPrefeitura = 1, Icone = "fa-light fa-user-nurse"},
                new Areadeservico {Id = 2, Nome = "Transporte", IdPrefeitura = 2, Icone = "fa-solid fa-bus"},
                new Areadeservico {Id = 3, Nome = "Esportes", IdPrefeitura = 2, Icone = "fa-solid fa-futbol"}
            };

            _context.AddRange(areasDeServico);
            _context.SaveChanges();

            _areaDeServicoService = new AreaDeServicoService(_context);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _areaDeServicoService.Create(new Areadeservico() { Id = 4, Nome = "Agricultura e Pecuária", IdPrefeitura = 3, Icone = "fa-solid fa-tractor" });

            // Assert
            Assert.AreEqual(4, _areaDeServicoService.GetAll().Count());
            var areaDeServico = _areaDeServicoService.Get(4);
            Assert.AreEqual("Agricultura e Pecuária", areaDeServico.Nome);
                
            Assert.AreEqual("fa-solid fa-tractor", areaDeServico.Icone);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            _areaDeServicoService.Delete(2);

            // Assert
            Assert.AreEqual(2, _areaDeServicoService.GetAll().Count());
            var areaDeServico = _areaDeServicoService.Get(2);
            Assert.AreEqual(null, areaDeServico);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Act 
            var areaDeServico = _areaDeServicoService.Get(3);
            areaDeServico.Nome = "Assistência Social";
            areaDeServico.Icone = "fa-solid fa-handshake-angle";
            _areaDeServicoService.Edit(areaDeServico);

            //Assert
            areaDeServico = _areaDeServicoService.Get(3);
            Assert.IsNotNull(areaDeServico);
            Assert.AreEqual("Assistência Social", areaDeServico.Nome);
            Assert.AreEqual("fa-solid fa-handshake-angle", areaDeServico.Icone);
        }

        [TestMethod()]
        public void GetTest()
        {
            var areaDeServico = _areaDeServicoService.Get(1);
            Assert.IsNotNull(areaDeServico);
            Assert.AreEqual("Saúde", areaDeServico.Nome);
            Assert.AreEqual("fa-light fa-user-nurse", areaDeServico.Icone);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var listaAreasDeServico = _areaDeServicoService.GetAll();

            // Assert
            Assert.IsInstanceOfType(listaAreasDeServico, typeof(IEnumerable<Areadeservico>));
            Assert.IsNotNull(listaAreasDeServico);
            Assert.AreEqual(3, listaAreasDeServico.Count());
            Assert.AreEqual(1, listaAreasDeServico.First().Id);
            Assert.AreEqual("Saúde", listaAreasDeServico.First().Nome);
        }
    }
}