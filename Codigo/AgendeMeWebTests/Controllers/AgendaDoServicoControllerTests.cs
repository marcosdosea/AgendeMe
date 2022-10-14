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
    public class AgendaDoServicoControllerTests
    {
        private static AgendaDoServicoController controller;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<IAgendaDoServicoService>();

            IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new AgendaDoServicoProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll()).Returns(GetTestAgendasDosServicos);
            mockService.Setup(service => service.Get(1))
                .Returns(GetTargetAgendaDoServico());
            mockService.Setup(service => service.Edit(It.IsAny<AgendaDoServico>()))
                .Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<AgendaDoServico>()))
                .Verifiable();
            controller = new AgendaDoServicoController(mockService.Object, mapper);
        }

        [TestMethod()]
        public void IndexTest()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<AgendaDoServicoViewModel>));

            List<AgendaDoServicoViewModel>? lista = (List<AgendaDoServicoViewModel>)viewResult.ViewData.Model;
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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AgendaDoServicoViewModel));
            AgendaDoServicoViewModel agendaDoServicoViewModel = (AgendaDoServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Quarta", agendaDoServicoViewModel.DiaSemana);
            Assert.AreEqual("09:30", agendaDoServicoViewModel.HorarioInicio);
            Assert.AreEqual(1, agendaDoServicoViewModel.VagasAtendimento);
            Assert.AreEqual(10, agendaDoServicoViewModel.VagasRetorno);
            Assert.AreEqual(1, agendaDoServicoViewModel.IdServicoPublico);
            Assert.AreEqual(1, agendaDoServicoViewModel.IdProfissional);
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
            var result = controller.Create(GetNewAgendaDoServico());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        public void CreateTest_Invalid()
        {
            // Arrange
            controller.ModelState.AddModelError("Nome", "Campo requerido");

            // Act
            var result = controller.Create(GetNewAgendaDoServico());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AgendaDoServicoViewModel));
            AgendaDoServicoViewModel agendaDoServicoViewModel = (AgendaDoServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Quarta", agendaDoServicoViewModel.DiaSemana);
            Assert.AreEqual("09:30", agendaDoServicoViewModel.HorarioInicio);
            Assert.AreEqual(1, agendaDoServicoViewModel.VagasAtendimento);
            Assert.AreEqual(10, agendaDoServicoViewModel.VagasRetorno);
            Assert.AreEqual(1, agendaDoServicoViewModel.IdServicoPublico);
            Assert.AreEqual(1, agendaDoServicoViewModel.IdProfissional);
        }

        [TestMethod()]
        public void EditTest_Post()
        {
            // Act
            var result = controller.Edit(GetTargetAgendaDoServicoViewModel().Id, GetTargetAgendaDoServicoViewModel());

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
            var result = controller.Delete(GetTargetAgendaDoServicoViewModel().Id, GetTargetAgendaDoServicoViewModel());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AgendaDoServicoViewModel));
            AgendaDoServicoViewModel agendaDoServicoViewModel = (AgendaDoServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Quarta", agendaDoServicoViewModel.DiaSemana);
            Assert.AreEqual("09:30", agendaDoServicoViewModel.HorarioInicio);
            Assert.AreEqual(1, agendaDoServicoViewModel.VagasAtendimento);
            Assert.AreEqual(10, agendaDoServicoViewModel.VagasRetorno);
            Assert.AreEqual(1, agendaDoServicoViewModel.IdServicoPublico);
            Assert.AreEqual(1, agendaDoServicoViewModel.IdProfissional);
        }

        private AgendaDoServicoViewModel GetNewAgendaDoServico()
        {
            return new AgendaDoServicoViewModel
            {
                Id = 1,
                DiaSemana = "Quarta",
                HorarioInicio = "09:30",
                HorarioFim = "10:00",
                VagasAtendimento = 1,
                VagasRetorno = 10,
                IdServicoPublico = 1,
                IdProfissional = 1
            };
        }

        private AgendaDoServicoViewModel GetTargetAgendaDoServicoViewModel()
        {
            return new AgendaDoServicoViewModel
            {
                Id = 2,
                DiaSemana = "Quarta",
                HorarioInicio = "09:30",
                HorarioFim = "10:00",
                VagasAtendimento = 1,
                VagasRetorno = 10,
                IdServicoPublico = 1,
                IdProfissional = 1
            };
        }

        private AgendaDoServico GetTargetAgendaDoServico()
        {
            return new AgendaDoServico
            {
                Id = 1,
                DiaSemana = "Quarta",
                HorarioInicio = "09:30",
                HorarioFim = "10:00",
                VagasAtendimento = 1,
                VagasRetorno = 10,
                IdServicoPublico = 1,
                IdProfissional = 1
            };
        }

        private IEnumerable<AgendaDoServico> GetTestAgendasDosServicos()
        {
            return new List<AgendaDoServico>
            {
                new AgendaDoServico
                {
                    Id = 1,
                    DiaSemana = "Quarta",
                    HorarioInicio = "09:30",
                    HorarioFim = "10:00",
                    VagasAtendimento = 1,
                    VagasRetorno = 10,
                    IdServicoPublico = 1,
                    IdProfissional = 1
                },
                new AgendaDoServico
                {
                    Id = 2,
                    DiaSemana = "Quinta",
                    HorarioInicio = "09:30",
                    HorarioFim = "10:00",
                    VagasAtendimento = 2,
                    VagasRetorno = 5,
                    IdServicoPublico = 1,
                    IdProfissional = 1
                }
            };
        }
    }
}