using AgendeMeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AgendeMeWeb.Controllers.Tests
{
    [TestClass()]
    public class AgendaDoServicoControllerTests
    {
        private static AgendaDoServicoController controller;
        
        [TestMethod()]
        public void AgendaDoServicoControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IndexTest()
        {
            Assert.Fail();
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