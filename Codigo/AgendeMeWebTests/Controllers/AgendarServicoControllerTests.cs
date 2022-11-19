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
    public class AgendarServicoControllerTests
    {
        private static AgendarServicoController controller;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mockServiceAgendamento = new Mock<IAgendamentoService>();
            var mockServicePrefeitura = new Mock<IPrefeituraService>();
            var mockServiceAreaDeServico = new Mock<IAreaDeServicoService>();
            var mockServiceServicoPublico = new Mock<IServicoPublicoService>();
            var mockServiceOrgaoPublico = new Mock<IOrgaoPublicoService>();
            var mockServiceDiaAgendamento = new Mock<IDiaAgendamentoService>();

            IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new AgendarServicoProfile())).CreateMapper();

            mockServiceAgendamento.Setup(service => service.GetAll()).Returns(GetTestAgendamentos);
            mockServiceAgendamento.Setup(service => service.Get(1))
                .Returns(GetTargetAgendamento());
            mockServiceAgendamento.Setup(service => service.Edit(It.IsAny<Agendamento>()))
                .Verifiable();
            mockServiceAgendamento.Setup(service => service.Create(It.IsAny<Agendamento>()))
                .Verifiable();

            mockServicePrefeitura.Setup(service => service.GetAll())
                .Returns(GetTestPrefeituras());

            /*mockServiceAreaDeServico.Setup(service => service.GetAll())
                .Returns(GetTestAreasDeServico());
            mockServiceAreaDeServico.Setup(service => service.Get(1))
                .Returns(GetTargetAreaDeServico());

            mockServiceServicoPublico.Setup(service => service.GetAll())
                .Returns(GetTestServicosPublico());
            mockServiceServicoPublico.Setup(service => service.Get(1))
                .Returns(GetTargetServicoPublico());

            mockServiceOrgaoPublico.Setup(service => service.GetAll())
                .Returns(GetTestOrgaosPublicos());
            mockServiceOrgaoPublico.Setup(service => service.Get(1))
                .Returns(GetTargetOrgaoPublico());

            mockServiceDiaAgendamento.Setup(service => service.GetAll())
                .Returns(GetTestDiasAgendamento());
            mockServiceDiaAgendamento.Setup(service => service.Get(1))
                .Returns(GetTargetDiaAgendamento());*/

            controller = new AgendarServicoController(mockServiceAgendamento.Object,
                                                      mockServicePrefeitura.Object,
                                                      mockServiceAreaDeServico.Object,
                                                      mockServiceServicoPublico.Object,
                                                      mockServiceOrgaoPublico.Object,
                                                      mockServiceDiaAgendamento.Object,
                                                      mapper);
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
        public void ListTest()
        {
            // Act
            var result = controller.List();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<AgendarServicoViewModel>));

            List<AgendarServicoViewModel>? lista = (List<AgendarServicoViewModel>)viewResult.ViewData.Model;
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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AgendarServicoViewModel));
            AgendarServicoViewModel agendarServicoViewModel = (AgendarServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Agendamento", agendarServicoViewModel.Tipo);
            Assert.AreEqual(1, agendarServicoViewModel.Id);
            Assert.AreEqual(2, agendarServicoViewModel.IdDiaAgendamento);
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
            var result = controller.Create(GetNewAgendarServico());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("List", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void CreateTest_Post_InValid()
        {
            // Arrange
            controller.ModelState.AddModelError("IdCidadao", "Campo requerido");

            // Act
            var result = controller.Create(GetNewAgendarServico());

            // Assert
            Assert.AreEqual(1, controller.ModelState.ErrorCount);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("List", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void EditTest_Get()
        {
            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AgendarServicoViewModel));
            AgendarServicoViewModel agendarServicoViewModel = (AgendarServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Agendamento", agendarServicoViewModel.Tipo);
            Assert.AreEqual(1, agendarServicoViewModel.Id);
            Assert.AreEqual(2, agendarServicoViewModel.IdDiaAgendamento);
        }

        [TestMethod()]
        public void EditTest_Post()
        {
            // Act
            var result = controller.Edit(GetNewAgendarServico().Id, GetNewAgendarServico());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void AtenderCidadaoTest()
        {
            // Act
            var result = controller.AtenderCidadao(GetNewAgendarServico().Id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AgendarServicoViewModel));
            AgendarServicoViewModel agendarServicoViewModel = (AgendarServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Agendamento", agendarServicoViewModel.Tipo);
            Assert.AreEqual(1, agendarServicoViewModel.Id);
            Assert.AreEqual(2, agendarServicoViewModel.IdDiaAgendamento);
        }


        [TestMethod()]
        public void DeleteTest_Get()
        {
            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AgendarServicoViewModel));
            AgendarServicoViewModel agendarServicoViewModel = (AgendarServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Agendamento", agendarServicoViewModel.Tipo);
            Assert.AreEqual(1, agendarServicoViewModel.Id);
            Assert.AreEqual(2, agendarServicoViewModel.IdDiaAgendamento);
        }

        [TestMethod()]
        public void DeleteTest_Post()
        {
            // Act
            var result = controller.Delete(GetNewAgendarServico().Id, GetNewAgendarServico());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void AgendarRetornoTest()
        {
            // Act
            var result = controller.AgendarRetorno(GetNewAgendarServico().Id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AgendarServicoViewModel));
            AgendarServicoViewModel agendarServicoViewModel = (AgendarServicoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Agendamento", agendarServicoViewModel.Tipo);
            Assert.AreEqual(1, agendarServicoViewModel.Id);
            Assert.AreEqual(2, agendarServicoViewModel.IdDiaAgendamento);
        }

        [TestMethod()]
        public void AjaxAreasDeServicoTest()
        {
            Assert.Fail();
        }

        private AgendarServicoViewModel GetNewAgendarServico()
        {
            return new AgendarServicoViewModel
            {
                Id = 4,
                Tipo = "Agendamento",
                Situacao = "Agendado",
                DataCadastro = DateTime.Now,
                IdCidadao = 1,
                IdDiaAgendamento = 1,
                IdAtendente = null,
                IdRetorno = null
            };

        }

        private Agendamento GetTargetAgendamento()
        {
            return new Agendamento
            {
                Id = 1,
                Tipo = "Agendamento",
                Situacao = "Agendado",
                DataCadastro = DateTime.Now,
                IdCidadao = 1,
                IdDiaAgendamento = 2,
                IdAtendente = null,
                IdRetorno = null
            };
        }

        private IEnumerable<Agendamento> GetTestAgendamentos()
        {
            return new List<Agendamento>
            {
                new Agendamento
                {
                    Id = 1,
                    Tipo = "Agendamento",
                    Situacao = "Agendado",
                    DataCadastro = DateTime.Now,
                    IdCidadao = 1,
                    IdDiaAgendamento = 1,
                    IdAtendente = null,
                    IdRetorno = null
                },
                new Agendamento
                {
                    Id = 2,
                    Tipo = "Agendamento",
                    Situacao = "Agendado",
                    DataCadastro = DateTime.Now,
                    IdCidadao = 1,
                    IdDiaAgendamento = 1,
                    IdAtendente = null,
                    IdRetorno = null
                },
                new Agendamento
                {
                    Id = 3,
                    Tipo = "Agendamento",
                    Situacao = "Agendado",
                    DataCadastro = DateTime.Now,
                    IdCidadao = 1,
                    IdDiaAgendamento = 1,
                    IdAtendente = null,
                    IdRetorno = null
                }
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
    }
}