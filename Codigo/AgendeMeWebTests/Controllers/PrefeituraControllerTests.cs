using AgendeMeWeb.Mappers;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AgendeMeWeb.Controllers.Tests
{
    [TestClass()]
    public class PrefeituraControllerTests
    {
        private static PrefeituraController controller;

        [TestInitialize()]
        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<IPrefeituraService>();

            IMapper mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new PrefeituraProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll())
                .Returns(GetTestPrefeituras());
            mockService.Setup(service => service.Get(1))
                .Returns(GetTargetPrefeitura());
            mockService.Setup(service => service.Edit(It.IsAny<Prefeitura>()))
                .Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Prefeitura>()))
                .Verifiable();
            controller = new PrefeituraController(mockService.Object, mapper);
        }

        private Prefeitura GetTargetPrefeitura()
        {
            return new Prefeitura
            {
                Id = 1,
                Nome = "Prefeitura de Campo do Brito",
                Cnpj = "84.833.489/0001-72",
                Estado = "SE",
                Cidade = "Salvador",
                Bairro = "Centro",
                Cep = "49520-000",
                Rua = "R. Padre Freire",
                Numero = "20",
                Icone = "Sem"

            };
        }

        private IEnumerable<Prefeitura> GetTestPrefeituras()
        {
            return new List<Prefeitura>
            {
                new Prefeitura
                {
                    Id = 1,
                    Nome = "Prefeitura de Aracaju",
                    Cnpj = "84.833.489/0001-72",
                    Estado = "SE",
                    Cidade = "Aracaju",
                    Bairro = "Ponto Novo",
                    Cep = "49097-270",
                    Rua = "R. Frei Luiz Canolo de Noronha",
                    Numero = "42",
                    Icone = "Sem"

                },
                new Prefeitura
                {
                    Id = 2,
                    Nome = "Prefeitura de Salvador",
                    Cnpj = "84.833.489/0001-72",
                    Estado = "BA",
                    Cidade = "Salvador",
                    Bairro = "Centro",
                    Cep = "40020-010",
                    Rua = "R. Frei Luiz Canolo de Noronha",
                    Numero = "s/n",
                    Icone = "Sem"

                },
                new Prefeitura
                {
                    Id = 3,
                    Nome = "Prefeitura de Campo do Brito",
                    Cnpj = "84.833.489/0001-72",
                    Estado = "SE",
                    Cidade = "Salvador",
                    Bairro = "Centro",
                    Cep = "49520-000",
                    Rua = "R. Padre Freire",
                    Numero = "20",
                    Icone = "Sem"

                }

            };
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
    }
}