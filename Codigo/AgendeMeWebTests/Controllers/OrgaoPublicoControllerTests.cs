using AgendeMeWeb.Mappers;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AgendeMeWeb.Controllers.Tests
{
    [TestClass()]
    public class OrgaoPublicoControllerTests
    {
        private static OrgaoPublicoController controller;

        [TestInitialize()]
        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<IOrgaoPublicoService>();

            IMapper mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new OrgaoPublicoProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll())
                .Returns(GetTestOrgaosPublicos());
            mockService.Setup(service => service.Get(1))
                .Returns(GetTargetOrgaoPublico());
            mockService.Setup(service => service.Edit(It.IsAny<Orgaopublico>()))
                .Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Orgaopublico>()))
                .Verifiable();
            controller = new OrgaoPublicoController(mockService.Object, mapper);
        }

        [TestMethod()]
        public void IndexTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DetailsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest1()
        {
            Assert.Fail();
        }

        private Orgaopublico GetTargetOrgaoPublico()
        {
            return new Orgaopublico
            {
                Id = 1,
                Nome = "OAB-Ordem dos Advogados do Brasil de Sergipe",
                Bairro = "Centro",
                Rua = "Av. Dr. Luiz Magalhães",
                Numero = "9",
                Complemento = "Zona Urbana",
                Cep = "49500-000",
                HoraAbre = "07:00",
                HoraFecha = "13:00",
                IdPrefeitura = 1
            };
        }

        private IEnumerable<Orgaopublico> GetTestOrgaosPublicos()
        {
            return new List<Orgaopublico>
            {
                new Orgaopublico
                {
                    Id = 1,
                    Nome = "OAB-Ordem dos Advogados do Brasil de Sergipe",
                    Bairro = "Centro",
                    Rua = "Av. Dr. Luiz Magalhães",
                    Numero = "9",
                    Complemento = "Zona Urbana",
                    Cep = "49500-000",
                    HoraAbre = "07:00",
                    HoraFecha = "13:00",
                    IdPrefeitura = 1
                },
                new Orgaopublico
                {
                    Id = 1,
                    Nome = "Hospital Regional de itabaiana Dr. Pedro Garcia Moreno",
                    Bairro = "Centro",
                    Rua = "Av. Treze de Junho",
                    Numero = "776",
                    Complemento = "Zona Urbana",
                    Cep = "49500-000",
                    HoraAbre = "07:00",
                    HoraFecha = "06:59",
                    IdPrefeitura = 1

                },
                new Orgaopublico
                {
                    Id = 1,
                    Nome = "Secretaria de Saúde de Itabaiana Sergipe",
                    Bairro = "Sítio Porto",
                    Rua = "Av. Ver. Olímpio Grande",
                    Numero = "00",
                    Complemento = "s/n",
                    Cep = "49500-000",
                    HoraAbre = "07:00",
                    HoraFecha = "13:00",
                    IdPrefeitura = 1
                }
            };
        }


    }
}