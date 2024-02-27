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
    public class DiaAgendamentoControllerTests
    {
        private static DiaAgendamentoController controller;

        [TestInitialize]
        public void Initialize()
        {
            // // Arrange
            // var mockService = new Mock<IDiaAgendamentoService>();

            // IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new DiaAgendamentoProfile())).CreateMapper();

            // mockService.Setup(service => service.GetAll()).Returns(GetTestDiaAgendamentos);
            // mockService.Setup(service => service.Get(1))
            //     .Returns(GetTargetDiaAgendamento());
            // mockService.Setup(service => service.Edit(It.IsAny<Diaagendamento>()))
            //     .Verifiable();
            // mockService.Setup(service => service.Create(It.IsAny<Diaagendamento>()))
            //     .Verifiable();
            // controller = new DiaAgendamentoController(mockService.Object, mapper);
        }

        [TestMethod()]
        public void IndexTest()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<DiaAgendamentoViewModel>));

            List<DiaAgendamentoViewModel>? lista = (List<DiaAgendamentoViewModel>)viewResult.ViewData.Model;
            Assert.AreEqual(2, lista.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DiaAgendamentoViewModel));
            DiaAgendamentoViewModel diaAgendamentoViewModel = (DiaAgendamentoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Quarta", diaAgendamentoViewModel?.DiaSemana);
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
        public void CreateTest_PostValid()
        {
            // Act
            var result = controller.Create(GetNewDiaAgendamento());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void CreateTest_PostInValid()
        {
            // Arrange
            controller.ModelState.AddModelError("DiaSemana", "Campo requerido");

            // Act
            var result = controller.Create(GetNewDiaAgendamento());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DiaAgendamentoViewModel));
            DiaAgendamentoViewModel diaAgendamentoViewModel = new();
            if (viewResult.ViewData.Model is not null)
                diaAgendamentoViewModel = (DiaAgendamentoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Quarta", diaAgendamentoViewModel.DiaSemana);
        }

        [TestMethod()]
        public void EditTest_Post()
        {
            // Act
            var result = controller.Edit(GetTargetDiaAgendamentoViewModel().Id, GetTargetDiaAgendamentoViewModel());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DiaAgendamentoViewModel));
            DiaAgendamentoViewModel diaAgendamentoViewModel = new();
            if (viewResult.ViewData.Model is not null)
                diaAgendamentoViewModel = (DiaAgendamentoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Quarta", diaAgendamentoViewModel.DiaSemana);
        }

        [TestMethod()]
        public void DeleteTest_Post()
        {
            // Act
            var result = controller.Delete(GetTargetDiaAgendamentoViewModel().Id, GetTargetDiaAgendamentoViewModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        private DiaAgendamentoViewModel GetNewDiaAgendamento()
        {
            return new DiaAgendamentoViewModel
            {
                Id = 1,
                Data = new DateTime(2023, 02, 27),
                DiaSemana = "Segunda",
                HorarioInicio = "08:00",
                HorarioFim = "12:00",
                VagasAtendimento = 10,
                VagasAgendadas = 0,
                VagasRetorno = 2,
                VagasAgendadasRetorno = 0,
                IdServicoPublico = 1
            };
        }

        private DiaAgendamentoViewModel GetTargetDiaAgendamentoViewModel()
        {
            return new DiaAgendamentoViewModel
            {
                Id = 2,
                Data = new DateTime(2023, 02, 28),
                DiaSemana = "Terça",
                HorarioInicio = "08:00",
                HorarioFim = "12:00",
                VagasAtendimento = 10,
                VagasAgendadas = 0,
                VagasRetorno = 2,
                VagasAgendadasRetorno = 0,
                IdServicoPublico = 1
            };
        }

        private Diaagendamento GetTargetDiaAgendamento()
        {
            return new Diaagendamento
            {
                Id = 1,
                Data = new DateTime(2023, 03, 01),
                DiaSemana = "Quarta",
                HorarioInicio = "08:00",
                HorarioFim = "12:00",
                VagasAtendimento = 10,
                VagasAgendadas = 0,
                VagasRetorno = 2,
                VagasAgendadasRetorno = 0,
                IdServicoPublico = 1
            };
        }

        private IEnumerable<Diaagendamento> GetTestDiaAgendamentos()
        {
            return new List<Diaagendamento>
            {
                new Diaagendamento
                {
                    Id = 1,
                    Data = new DateTime(2023,03,01),
                    DiaSemana = "Quarta",
                    HorarioInicio = "08:00",
                    HorarioFim = "12:00",
                    VagasAtendimento = 10,
                    VagasAgendadas = 0,
                    VagasRetorno = 2,
                    VagasAgendadasRetorno = 0,
                    IdServicoPublico = 1
                },
                new Diaagendamento
                {
                    Id = 2,
                    Data = new DateTime(2023,03,02),
                    DiaSemana = "Quinta",
                    HorarioInicio = "08:00",
                    HorarioFim = "12:00",
                    VagasAtendimento = 10,
                    VagasAgendadas = 0,
                    VagasRetorno = 2,
                    VagasAgendadasRetorno = 0,
                    IdServicoPublico = 1
                }
            };
        }
    }
}