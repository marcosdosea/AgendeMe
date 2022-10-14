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
    public class AreaDeServicoControllerTests
    {
        private static AreaDeServicoController controller;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<IAreaDeServicoService>();

            IMapper mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new AreaDeServicoProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll())
                .Returns(GetTestAreasDeServico());
            mockService.Setup(service => service.Get(1))
                .Returns(GetTargetAreaDeServico());
            mockService.Setup(service => service.Edit(It.IsAny<Areadeservico>()))
                .Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Areadeservico>()))
                .Verifiable();
            controller = new AreaDeServicoController(mockService.Object, mapper);
        }


        [TestMethod()]
        public void IndexTest()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<AreaDeServicoViewModel>));

            List<AreaDeServicoViewModel> lista = (List<AreaDeServicoViewModel>)viewResult.ViewData.Model;
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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AreaDeServicoViewModel));
            AreaDeServicoViewModel areaDeServicoViewModel = (AreaDeServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Saúde", areaDeServicoViewModel.Nome);
            Assert.AreEqual(1, areaDeServicoViewModel.IdPrefeitura);
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
            var result = controller.Create(GetNewAreaDeServico());

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
            var result = controller.Create(GetNewAreaDeServico());

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
            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AreaDeServicoViewModel));
            AreaDeServicoViewModel areaDeServicoViewModel = (AreaDeServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Saúde", areaDeServicoViewModel.Nome);
            Assert.AreEqual(1, areaDeServicoViewModel.IdPrefeitura);
        }

        [TestMethod()]
        public void EditTest_Post()
        {
            // Act
            var result = controller.Edit(GetTargetAreaDeServicoViewModel().Id, GetTargetAreaDeServicoViewModel());

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
            var result = controller.Delete(GetTargetAreaDeServicoViewModel().Id, GetTargetAreaDeServicoViewModel());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AreaDeServicoViewModel));
            AreaDeServicoViewModel areaDeServicoViewModel = (AreaDeServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Saúde", areaDeServicoViewModel.Nome);
            Assert.AreEqual(1, areaDeServicoViewModel.IdPrefeitura);
        }
        private AreaDeServicoViewModel GetNewAreaDeServico()
        {
            return new AreaDeServicoViewModel
            {
                Id = 4,
                Nome = "Agricultura e Meio Ambiente",
                Icone = "qualquer icone",
                IdPrefeitura = 1
            };

        }
        private Areadeservico GetTargetAreaDeServico()
        {
            return new Areadeservico
            {
                Id = 1,
                Nome = "Saúde",
                Icone = "qualquer icone",
                IdPrefeitura = 1
            };
        }

        private AreaDeServicoViewModel GetTargetAreaDeServicoViewModel()
        {
            return new AreaDeServicoViewModel
            {
                Id = 2,
                Nome = "Transporte",
                Icone = "qualquer icone",
                IdPrefeitura = 1
            };
        }

        private IEnumerable<Areadeservico> GetTestAreasDeServico()
        {
            return new List<Areadeservico>
            {
                new Areadeservico
                {
                    Id = 1,
                    Nome = "Saúde",
                    Icone = "qualquer icone",
                    IdPrefeitura = 1
                },
                new Areadeservico
                {
                    Id = 2,
                    Nome = "Transporte",
                    Icone = "qualquer icone",
                    IdPrefeitura = 1
                },
                new Areadeservico
                {
                    Id = 3,
                    Nome = "Esporte",
                    Icone = "qualquer icone",
                    IdPrefeitura = 1
                }
            };
        }
    }
}