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
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<OrgaoPublicoViewModel>));

            List<OrgaoPublicoViewModel>? lista = (List<OrgaoPublicoViewModel>)viewResult.ViewData.Model;
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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(OrgaoPublicoViewModel));
            OrgaoPublicoViewModel orgaoPublicoViewModel = (OrgaoPublicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("OAB-Ordem dos Advogados do Brasil de Sergipe", orgaoPublicoViewModel.Nome);
            Assert.AreEqual("Centro", orgaoPublicoViewModel.Bairro);
            Assert.AreEqual("Av. Dr. Luiz Magalhães", orgaoPublicoViewModel.Rua);
            Assert.AreEqual("9", orgaoPublicoViewModel.Numero);
            Assert.AreEqual("Zona Urbana", orgaoPublicoViewModel.Complemento);
            Assert.AreEqual("49500-000", orgaoPublicoViewModel.Cep);
            Assert.AreEqual("07:00", orgaoPublicoViewModel.HoraAbre);
            Assert.AreEqual("13:00", orgaoPublicoViewModel.HoraFecha);
            Assert.AreEqual(1, orgaoPublicoViewModel.IdPrefeitura);
        }

        [TestMethod()]
        public void CreateTest_Test()
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
            var result = controller.Create(GetNewOrgaoPublico());

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
            var result = controller.Create(GetNewOrgaoPublico());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(OrgaoPublicoViewModel));
            OrgaoPublicoViewModel orgaoPublicoViewModel = (OrgaoPublicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("OAB-Ordem dos Advogados do Brasil de Sergipe", orgaoPublicoViewModel.Nome);
            Assert.AreEqual("Centro", orgaoPublicoViewModel.Bairro);
            Assert.AreEqual("Av. Dr. Luiz Magalhães", orgaoPublicoViewModel.Rua);
            Assert.AreEqual("9", orgaoPublicoViewModel.Numero);
            Assert.AreEqual("Zona Urbana", orgaoPublicoViewModel.Complemento);
            Assert.AreEqual("49500-000", orgaoPublicoViewModel.Cep);
            Assert.AreEqual("07:00", orgaoPublicoViewModel.HoraAbre);
            Assert.AreEqual("13:00", orgaoPublicoViewModel.HoraFecha);
            Assert.AreEqual(1, orgaoPublicoViewModel.IdPrefeitura);
        }

        [TestMethod()]
        public void EditTest_Post()
        {
            // Act
            var result = controller.Edit(GetTargetOrgaoPublicoViewModel().Id, GetTargetOrgaoPublicoViewModel());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(OrgaoPublicoViewModel));
            OrgaoPublicoViewModel orgaoPublicoViewModel = (OrgaoPublicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("OAB-Ordem dos Advogados do Brasil de Sergipe", orgaoPublicoViewModel.Nome);
            Assert.AreEqual("Centro", orgaoPublicoViewModel.Bairro);
            Assert.AreEqual("Av. Dr. Luiz Magalhães", orgaoPublicoViewModel.Rua);
            Assert.AreEqual("9", orgaoPublicoViewModel.Numero);
            Assert.AreEqual("Zona Urbana", orgaoPublicoViewModel.Complemento);
            Assert.AreEqual("49500-000", orgaoPublicoViewModel.Cep);
            Assert.AreEqual("07:00", orgaoPublicoViewModel.HoraAbre);
            Assert.AreEqual("13:00", orgaoPublicoViewModel.HoraFecha);
            Assert.AreEqual(1, orgaoPublicoViewModel.IdPrefeitura);
        }

        [TestMethod()]
        public void DeleteTest_Get()
        {
            // Act
            var result = controller.Delete(GetTargetOrgaoPublicoViewModel().Id, GetTargetOrgaoPublicoViewModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
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

        private OrgaoPublicoViewModel GetNewOrgaoPublico()
        {
            return new OrgaoPublicoViewModel
            {
                Id = 4,
                Nome = "Clínica de Saúde da Família Raimunda Ribeiro dos Santos",
                Bairro = "Centro",
                Rua = "Rua Roque Bispo de Menezes",
                Numero = "s/n",
                Complemento = "Zona Urbana",
                Cep = "49565-000",
                HoraAbre = "07:00",
                HoraFecha = "15:00",
                IdPrefeitura = 2
            };
        }

        private OrgaoPublicoViewModel GetTargetOrgaoPublicoViewModel()
        {
            return new OrgaoPublicoViewModel
            {
                Id = 7,
                Nome = "Clínica de Saúde da Família Raimunda Ribeiro dos Santos",
                Bairro = "Centro",
                Rua = "Rua Roque Bispo de Menezes",
                Numero = "s/n",
                Complemento = "Zona Urbana",
                Cep = "49565-000",
                HoraAbre = "07:00",
                HoraFecha = "15:00",
                IdPrefeitura = 2
            };
        }


    }
}