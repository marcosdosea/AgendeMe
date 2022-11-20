using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass()]
    public class ServicoPublicoServiceTests
    {
        private AgendeMeContext _context;
        private IServicoPublicoService _autorService;

        [TestInitialize()]
        public void Initialize()
        {
            //Arrange
            var builder = new DbContextOptionsBuilder<AgendeMeContext>();
            builder.UseInMemoryDatabase("AgendeMe");
            var options = builder.Options;

            _context = new AgendeMeContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var servicosPublicos = new List<Servicopublico>
                {
                    new Servicopublico { Id = 1, Nome = "Médico", IdArea = 1, IdOrgaoPublico = 1, Icone = "Sem"},
                    new Servicopublico { Id = 2, Nome = "Transporte coletivo", IdArea = 2, IdOrgaoPublico = 2, Icone = "Sem"},
                    new Servicopublico { Id = 3, Nome = "Distribuição de medicamentos", IdArea = 1, IdOrgaoPublico = 1, Icone = "Sem"},
                };

            _context.AddRange(servicosPublicos);
            _context.SaveChanges();

            _autorService = new ServicoPublicoService(_context);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _autorService.Create(new Servicopublico() { Id = 4, Nome = "Serviço funerário", IdArea = 3, IdOrgaoPublico = 3, Icone = "Sem" });
            // Assert
            Assert.AreEqual(4, _autorService.GetAll().Count());
            var servicoPublico = _autorService.Get(4);
            Assert.AreEqual("Serviço funerário", servicoPublico.Nome);
            Assert.AreEqual(3, servicoPublico.IdArea);
            Assert.AreEqual(3, servicoPublico.IdOrgaoPublico);
            Assert.AreEqual("Sem", servicoPublico.Icone);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            _autorService.Delete(2);
            // Assert
            Assert.AreEqual(2, _autorService.GetAll().Count());
            var servicoPublico = _autorService.Get(3);
            Assert.AreEqual("Distribuição de medicamentos", servicoPublico.Nome);
            Assert.AreEqual(1, servicoPublico.IdArea);
            Assert.AreEqual(1, servicoPublico.IdOrgaoPublico);
            Assert.AreEqual("Sem", servicoPublico.Icone);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Act 
            var servicoPublico = _autorService.Get(1);
            servicoPublico.Nome = "Serviços básicos de saúde";
            _autorService.Edit(servicoPublico);
            //Assert
            servicoPublico = _autorService.Get(1);
            Assert.IsNotNull(servicoPublico);
            Assert.AreEqual("Serviços básicos de saúde", servicoPublico.Nome);
        }

        [TestMethod()]
        public void GetTest()
        {
            var servicoPublico = _autorService.Get(2);
            Assert.IsNotNull(servicoPublico);
            Assert.AreEqual("Transporte coletivo", servicoPublico.Nome);
            Assert.AreEqual(2, servicoPublico.IdArea);
            Assert.AreEqual(2, servicoPublico.IdOrgaoPublico);
            Assert.AreEqual("Sem", servicoPublico.Icone);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var listaServicoPublico = _autorService.GetAll();
            // Assert
            Assert.IsInstanceOfType(listaServicoPublico, typeof(IEnumerable<Servicopublico>));
            Assert.IsNotNull(listaServicoPublico);
            Assert.AreEqual(3, listaServicoPublico.Count());
            Assert.AreEqual(1, listaServicoPublico.First().Id);
            Assert.AreEqual("Médico", listaServicoPublico.First().Nome);
        }
    }
}