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
    public class CargoControllerTests
    {
        private static CargoController controller;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<ICargoService>();

            IMapper mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new CargoProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll())
                .Returns(GetTestCargos());

            mockService.Setup(service => service.Get(1))
                .Returns(GetTargetCargo());

            mockService.Setup(service => service.Edit(It.IsAny<Cargo>()))
                .Verifiable();

            mockService.Setup(service => service.Create(It.IsAny<Cargo>()))
                .Verifiable();

            controller = new CargoController(mockService.Object, mapper);
        }

        [TestMethod()]
        public void IndexTest()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<CargoViewModel>));

            List<CargoViewModel>? lista = (List<CargoViewModel>)viewResult.ViewData.Model;
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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CargoViewModel));
            CargoViewModel cargoViewModel = (CargoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Motorista", cargoViewModel.Nome);
            Assert.AreEqual("Dirige os veículos", cargoViewModel.Descricao);
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
            var result = controller.Create(GetNewCargo());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void CreateTest_InValid()
        {
            // Arrange
            controller.ModelState.AddModelError("Nome", "Campo requerido");

            // Act
            var result = controller.Create(GetNewCargo());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CargoViewModel));
            CargoViewModel cargoViewModel = (CargoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Motorista", cargoViewModel.Nome);
            Assert.AreEqual("Dirige os veículos", cargoViewModel.Descricao);
        }

        [TestMethod()]
        public void EditTest_Post()
        {
            // Act
            var result = controller.Edit(GetTargetCargoViewModel().Id, GetTargetCargoViewModel());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CargoViewModel));
            CargoViewModel cargoViewModel = (CargoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Motorista", cargoViewModel.Nome);
            Assert.AreEqual("Dirige os veículos", cargoViewModel.Descricao);
        }

        [TestMethod()]
        public void DeleteTest_Get()
        {
            // Act
            var result = controller.Delete(GetTargetCargoViewModel().Id, GetTargetCargoViewModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }


        private CargoViewModel GetNewCargo()
        {
            return new CargoViewModel
            {
                Id = 4,
                Nome = "Cardiologista",
                Descricao = "Especialista no tratamento de doenças que afeta o coração."
            };

        }

        private Cargo GetTargetCargo()
        {
            return new Cargo
            {
                Id = 1,
                Nome = "Motorista",
                Descricao = "Dirige os veículos"
            };
        }

        private CargoViewModel GetTargetCargoViewModel()
        {
            return new CargoViewModel
            {
                Id = 2,
                Nome = "Personal trainer",
                Descricao = "Professor de educação física"
            };
        }

        private IEnumerable<Cargo> GetTestCargos()
        {
            return new List<Cargo>
            {
                new Cargo
                {
                    Id = 1,
                    Nome = "Motorista",
                    Descricao = "Dirige os veículos"
                },
                new Cargo
                {
                    Id = 2,
                    Nome = "Personal trainer",
                    Descricao = "Professor de educação física"
                },
                new Cargo
                {
                    Id = 3,
                    Nome = "Advogado",
                    Descricao = "Responsável por defender os interresses dos cidadãos, usando o conhecimento das leis para esta finalidade."
                },
            };
        }
    }
}