using AgendeMeWeb.Mappers;
using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
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
            mockService.Setup(service => service.Delete(1)).Verifiable();
            controller = new PrefeituraController(mockService.Object, mapper);
        }


        [TestMethod()]
        public void IndexTest()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<PrefeituraViewModel>));

            List<PrefeituraViewModel>? lista = (List<PrefeituraViewModel>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            // Act
            var result = controller.Details(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(PrefeituraViewModel));
            PrefeituraViewModel prefeituraViewModel = (PrefeituraViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Campo do Brito", prefeituraViewModel.Nome);
            Assert.AreEqual("84.833.489/0001-72", prefeituraViewModel.Cnpj);
            Assert.AreEqual("SE", prefeituraViewModel.Estado);
            Assert.AreEqual("Campo do Brito", prefeituraViewModel.Cidade);
            Assert.AreEqual("Centro", prefeituraViewModel.Bairro);
            Assert.AreEqual("49520-000", prefeituraViewModel.Cep);
            Assert.AreEqual("R. Padre Freire", prefeituraViewModel.Rua);
            Assert.AreEqual("20", prefeituraViewModel.Numero);
            Assert.AreEqual("Sem", prefeituraViewModel.Icone);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            var result = controller.Create();
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void CreateTest_Valid()
        {
            // Act
            var result = controller.Create(GetNewPrefeitura());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void EditTest_Get()
        {
            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(PrefeituraViewModel));
            PrefeituraViewModel prefeituraViewModel = (PrefeituraViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Campo do Brito", prefeituraViewModel.Nome);
            Assert.AreEqual("84.833.489/0001-72", prefeituraViewModel.Cnpj);
            Assert.AreEqual("SE", prefeituraViewModel.Estado);
            Assert.AreEqual("Prefeitura de Campo do Brito", prefeituraViewModel.Cidade);
            Assert.AreEqual("Centro", prefeituraViewModel.Bairro);
            Assert.AreEqual("49520-000", prefeituraViewModel.Cep);
            Assert.AreEqual("R. Padre Freire", prefeituraViewModel.Rua);
            Assert.AreEqual("20", prefeituraViewModel.Numero);
            Assert.AreEqual("Sem", prefeituraViewModel.Icone);
        }

        [TestMethod()]
        public void EditTest_Post()
        {
            // Act
            var result = controller.Edit(GetTargetPrefeituraViewModel().Id, GetTargetPrefeituraViewModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void DeleteTest_Post()
        {
            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(PrefeituraViewModel));
            PrefeituraViewModel prefeituraViewModel = (PrefeituraViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Campo do Brito", prefeituraViewModel.Nome);
            Assert.AreEqual("84.833.489/0001-72", prefeituraViewModel.Cnpj);
            Assert.AreEqual("SE", prefeituraViewModel.Estado);
            Assert.AreEqual("Campo do Brito", prefeituraViewModel.Cidade);
            Assert.AreEqual("Centro", prefeituraViewModel.Bairro);
            Assert.AreEqual("49520-000", prefeituraViewModel.Cep);
            Assert.AreEqual("R. Padre Freire", prefeituraViewModel.Rua);
            Assert.AreEqual("20", prefeituraViewModel.Numero);
            Assert.AreEqual("Sem", prefeituraViewModel.Icone);
        }

        [TestMethod()]
        public void DeleteTest_Get()
        {
            // Act
            var result = controller.Delete(GetTargetPrefeituraViewModel().Id, GetTargetPrefeituraViewModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        private Prefeitura GetTargetPrefeitura()
        {
            return new Prefeitura
            {
                Id = 1,
                Nome = "Prefeitura de Campo do Brito",
                Cnpj = "84.833.489/0001-72",
                Estado = "SE",
                Cidade = "Campo do Brito",
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
                    Cidade = "Campo do Brito",
                    Bairro = "Centro",
                    Cep = "49520-000",
                    Rua = "R. Padre Freire",
                    Numero = "20",
                    Icone = "Sem"

                }

            };
        }

        private PrefeituraViewModel GetNewPrefeitura()
        {
            return new PrefeituraViewModel
            {
                Id = 4,
                Nome = "Prefeitura de Lagarto",
                Cnpj = "84.833.489/0001-72",
                Estado = "SE",
                Cidade = "Lagarto",
                Bairro = "Centro",
                Cep = "49400-000",
                Rua = "Pr. da Piedade",
                Numero = "13",
                Icone = "Sem"

            };

        }

        private PrefeituraViewModel GetTargetPrefeituraViewModel()
        {
            return new PrefeituraViewModel
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

            };
        }


    }
}