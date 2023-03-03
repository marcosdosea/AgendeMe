using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass()]
    public class OrgaoPublicoServiceTests
    {
        private AgendeMeContext _context;
        private IOrgaoPublicoService _orgaoPublicoService;

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
            var orgaosPublicos = new List<Orgaopublico>
                {
                    new Orgaopublico { Id = 1, Nome = "Posto de Saúde Souto Diniz", Bairro = "Centro", Rua = "Av. Otoniel Dória", Numero = "534", Complemento = "Centro da cidade", Cep = "49500-000", HoraAbre = "07:00", HoraFecha = "17:00", IdPrefeitura = 1},
                    new Orgaopublico { Id  = 2, Nome = "Clínica de Saúde da Família Raimunda Ribeiro dos Santo", Bairro = "Centro", Numero = "s/n", Complemento = "Na rua do posto de combustíveis", Cep = "49565-000", HoraAbre = "07:00", HoraFecha = "17:00", IdPrefeitura = 2},
                    new Orgaopublico { Id = 3, Nome = "Centro de Saúde Dr. Lauro Maia", Bairro = " Mamede Paes Mendonça", Rua = "R. Percílio Andrade", Numero = "1633", Complemento = "s/n", Cep = "49500-000", HoraAbre = "06:30", HoraFecha = "17:00", IdPrefeitura = 1}
                };

            _context.AddRange(orgaosPublicos);
            _context.SaveChanges();

            _orgaoPublicoService = new OrgaoPublicoService(_context);
        }


            [TestMethod()]
        public void CreateTest()
        {
            // Act
            _orgaoPublicoService.Create(new Orgaopublico() { Id = 4, Nome = "Clínica Nossa Senhora da Boa Hora", Bairro = "Centro", Rua = "R. Rodrigues Dória", Numero = "430", Complemento = "s/n", Cep = "49520-000", HoraAbre = "06:00", HoraFecha = "17:00", IdPrefeitura = 3 });
                // Assert
            Assert.AreEqual(4, _orgaoPublicoService.GetAll().Count());
            var orgaoPublico = _orgaoPublicoService.Get(4);
            Assert.AreEqual("Clínica Nossa Senhora da Boa Hora", orgaoPublico.Nome);
            Assert.AreEqual("Centro", orgaoPublico.Bairro);
            Assert.AreEqual("R. Rodrigues Dória", orgaoPublico.Rua);
            Assert.AreEqual("430", orgaoPublico.Numero);
            Assert.AreEqual("s/n", orgaoPublico.Complemento);
            Assert.AreEqual("49520-000", orgaoPublico.Cep);
            Assert.AreEqual("06:00", orgaoPublico.HoraAbre);
            Assert.AreEqual("17:00", orgaoPublico.HoraFecha);
            Assert.AreEqual(3, orgaoPublico.IdPrefeitura);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            _orgaoPublicoService.Delete(2);
            // Assert
            Assert.AreEqual(2, _orgaoPublicoService.GetAll().Count());
            var orgaoPublico = _orgaoPublicoService.Get(2);
            Assert.AreEqual(null, orgaoPublico);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Act 
            var orgaoPublico = _orgaoPublicoService.Get(3);
            orgaoPublico.Numero = "3000";
            _orgaoPublicoService.Edit(orgaoPublico);
            //Assert
            orgaoPublico = _orgaoPublicoService.Get(3);
            Assert.IsNotNull(orgaoPublico);
            Assert.AreEqual("3000", orgaoPublico.Numero);
        }

        [TestMethod()]
        public void GetTest()
        {
            var orgaoPublico = _orgaoPublicoService.Get(1);
            Assert.IsNotNull(orgaoPublico);
            Assert.AreEqual("Posto de Saúde Souto Diniz", orgaoPublico.Nome);
            Assert.AreEqual("Centro", orgaoPublico.Bairro);
            Assert.AreEqual("Av. Otoniel Dória", orgaoPublico.Rua);
            Assert.AreEqual("534", orgaoPublico.Numero);
            Assert.AreEqual("Centro da cidade", orgaoPublico.Complemento);
            Assert.AreEqual("49500-000", orgaoPublico.Cep);
            Assert.AreEqual("07:00", orgaoPublico.HoraAbre);
            Assert.AreEqual("17:00", orgaoPublico.HoraFecha);
            Assert.AreEqual(1, orgaoPublico.IdPrefeitura);
        }
    

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var listaAutor = _orgaoPublicoService.GetAll();
            // Assert
            Assert.IsInstanceOfType(listaAutor, typeof(IEnumerable<Orgaopublico>));
            Assert.IsNotNull(listaAutor);
            Assert.AreEqual(3, listaAutor.Count());
            Assert.AreEqual(1, listaAutor.First().Id);
            Assert.AreEqual("Posto de Saúde Souto Diniz", listaAutor.First().Nome);
        }
    }
}