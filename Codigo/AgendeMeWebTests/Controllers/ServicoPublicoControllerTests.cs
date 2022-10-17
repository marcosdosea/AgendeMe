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
    public class ServicoPublicoControllerTests
    {
        private static ServicoPublicoController controller;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<IServicoPublicoService>();

            IMapper mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new ServicoPublicoProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll())
                .Returns(GetTestServicosPublico());
            mockService.Setup(service => service.Get(1))
                .Returns(GetTargetServicoPublico());
            mockService.Setup(service => service.Edit(It.IsAny<Servicopublico>()))
                .Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Servicopublico>()))
                .Verifiable();
            controller = new ServicoPublicoController(mockService.Object, mapper);
        }

        [TestMethod()]
        public void IndexTest()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<ServicoPublicoViewModel>));

            List<ServicoPublicoViewModel> lista = (List<ServicoPublicoViewModel>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ServicoPublicoViewModel));
            ServicoPublicoViewModel servicoPublicoViewModel = (ServicoPublicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Clínico Geral", servicoPublicoViewModel.Nome);
            Assert.AreEqual(1, servicoPublicoViewModel.IdArea);
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
        public void CreateTest_Post_Valid()
        {
            // Act
            var result = controller.Create(GetNewServicoPublico());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void CreateTest_Post_InValid()
        {
            // Arrange
            controller.ModelState.AddModelError("Nome", "Campo requerido");

            // Act
            var result = controller.Create(GetNewServicoPublico());

            // Assert
            Assert.AreEqual(1, controller.ModelState.ErrorCount);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }


        [TestMethod()]
        public void EditTest_Get()
        {
            //Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ServicoPublicoViewModel));
            ServicoPublicoViewModel servicoPublicoViewModel = (ServicoPublicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Clínico Geral", servicoPublicoViewModel.Nome);
            Assert.AreEqual(1, servicoPublicoViewModel.IdArea);
        }

        [TestMethod()]
        public void EditTest_Post()
        {
            // Act
            var result = controller.Edit(GetTargetServicoPublicoViewModel().Id, GetTargetServicoPublicoViewModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void DeleteTest_Get()
        {
            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ServicoPublicoViewModel));
            ServicoPublicoViewModel servicoPublicoViewModel = (ServicoPublicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Clínico Geral", servicoPublicoViewModel.Nome);
            Assert.AreEqual(1, servicoPublicoViewModel.IdArea);
        }

        [TestMethod()]
        public void DeleteTest_Post()
        {
            // Act
            var result = controller.Delete(GetTargetServicoPublicoViewModel().Id, GetTargetServicoPublicoViewModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        private ServicoPublicoViewModel GetNewServicoPublico()
        {
            return new ServicoPublicoViewModel
            {
                Id = 4,
                Nome = "Cardiologista",
                Icone = "qualquer icone",
                IdArea = 1,
                IdOrgaoPublico = 1
            };
        }

        private Servicopublico GetTargetServicoPublico()
        {
            return new Servicopublico
            {
                Id = 1,
                Nome = "Clínico Geral",
                Icone = "qualquer icone",
                IdArea = 1,
                IdOrgaoPublico = 1
            };
        }

        private ServicoPublicoViewModel GetTargetServicoPublicoViewModel()
        {
            return new ServicoPublicoViewModel
            {
                Id = 2,
                Nome = "Nutricionista",
                Icone = "qualquer icone",
                IdArea = 1,
                IdOrgaoPublico = 1
            };
        }

        private IEnumerable<Servicopublico> GetTestServicosPublico()
        {
            return new List<Servicopublico>
            {
                new Servicopublico
                {
                    Id = 1,
                    Nome = "Clínico Geral",
                    Icone = "qualquer icone",
                    IdArea = 1,
                    IdOrgaoPublico = 1
                },
                new Servicopublico
                {
                    Id = 2,
                    Nome = "Nutricionista",
                    Icone = "qualquer icone",
                    IdArea = 1,
                    IdOrgaoPublico = 1
                },
                new Servicopublico
                {
                    Id = 2,
                    Nome = "Transporte Público",
                    Icone = "qualquer icone",
                    IdArea = 2,
                    IdOrgaoPublico = 2
                }
            };
        }
    }
}