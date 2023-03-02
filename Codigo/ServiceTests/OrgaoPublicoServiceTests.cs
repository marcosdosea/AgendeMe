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
        private IOrgaoPublicoService _autorService;

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
                    new Orgaopublico { },
                    new Orgaopublico { }
                };

            _context.AddRange(orgaosPublicos);
            _context.SaveChanges();

            _orgaoPublicoService = new OrgaoPublicoService(_context);
        }


            [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            Assert.Fail();
        }
    }
}