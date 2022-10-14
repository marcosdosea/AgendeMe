using AgendeMeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgendeMeWeb.Controllers.Tests
{
    [TestClass()]
    public class AgendaDoServicoControllerTests
    {
        private static AgendaDoServicoController controller;

        [TestInitialize]
        public void Initialize()
        {
            Assert.Fail();
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
            Assert.AreEqual(1, lista.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(AgendaDoServicoController));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AgendaDoServicoController));
            AgendaDoServicoViewModel agendaDoServicoViewModel = (AgendaDoServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Quarta", agendaDoServicoViewModel.DiaSemana);

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