using AgendeMeWeb.Mappers;
using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.DTO;
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
            var mockServiceCidadao = new Mock<ICidadaoService>();

            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new AgendarServicoProfile());
                cfg.AddProfile(new PrefeituraProfile());
                cfg.AddProfile(new AreaDeServicoProfile());
            });
            IMapper mapper = new Mapper(mapperConfig);

            mockServiceAgendamento.Setup(service => service.GetAll()).Returns(GetTestAgendamentos);
            mockServiceAgendamento.Setup(service => service.Get(1))
                .Returns(GetTargetAgendamento());
            mockServiceAgendamento.Setup(service => service.GetDados(1))
                .Returns(GetTargetAgendamentoDTO());
            mockServiceAgendamento.Setup(service => service.Edit(It.IsAny<Agendamento>()))
                .Verifiable();
            mockServiceAgendamento.Setup(service => service.Create(It.IsAny<Agendamento>()))
                .Verifiable();

            mockServicePrefeitura.Setup(service => service.GetAll())
                .Returns(GetTestPrefeituras());


            mockServiceAreaDeServico.Setup(service => service.GetAllByIdPrefeitura(1))
                .Returns(GetTestAreasDeServico());

            mockServiceServicoPublico.Setup(service => service.GetAllByIdArea(1))
                .Returns(GetTestServicosPublico());

            mockServiceOrgaoPublico.Setup(service => service.GetAllByNomeServicoPublico("Clínico Geral"))
                .Returns(GetTestOrgaosPublicos());

            mockServiceDiaAgendamento.Setup(service => service.GetAllDiasByIdServico(1))
                .Returns(GetTestAgendamentoDias());
            mockServiceDiaAgendamento.Setup(service => service.GetAllHorasByIdServicoAndDia(1, new DateTime(2022, 10, 09)))
                .Returns(GetTestAgendamentoHoras());
            mockServiceDiaAgendamento.Setup(service => service.GetDadosAgendamento(1))
                .Returns(GetTargetConfirmarAgendamento());

            mockServiceCidadao.Setup(service => service.GetByCPF("000.000.000-00"))
                .Returns(GetTargetCidadao());

            controller = new AgendarServicoController(mockServiceAgendamento.Object,
                                                      mockServicePrefeitura.Object,
                                                      mockServiceAreaDeServico.Object,
                                                      mockServiceServicoPublico.Object,
                                                      mockServiceOrgaoPublico.Object,
                                                      mockServiceDiaAgendamento.Object,
                                                      mockServiceCidadao.Object,
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
            var result = controller.AtenderCidadao(1);

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
            var result = controller.AgendarRetorno(1);

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
        public void AreasDeServicoTest()
        {
            // Act
            var result = controller.AreasDeServico(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<AreaDeServicoViewModel>));

            List<AreaDeServicoViewModel> lista = (List<AreaDeServicoViewModel>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public void ServicosPublicosTest()
        {
            // Act
            var result = controller.ServicosPublicos(1, "Saúde", "Icone");

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<ServicoPublicoDTO>));

            List<ServicoPublicoDTO> lista = (List<ServicoPublicoDTO>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public void OrgaosPublicosTest()
        {
            // Act
            var result = controller.OrgaosPublicos("Clínico Geral", "Icone");

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<OrgaoPublicoDTO>));

            List<OrgaoPublicoDTO> lista = (List<OrgaoPublicoDTO>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public void AgendarServicoDiasTest()
        {
            // Act
            var result = controller.AgendarServicoDias(1,
                "Hospital Regional de Itabaiana",
                "Clínico Geral", 1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<AgendamentoDiasDTO>));

            List<AgendamentoDiasDTO> lista = (List<AgendamentoDiasDTO>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public void AgendarServicoHorasTest()
        {
            // Act
            var result = controller.AgendarServicoHoras(1,
                new DateTime(2022, 10, 09),
                "Segunda",
                "Hospital Regional de Itabaiana",
                "Clínico Geral",
                1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<AgendamentoHorasDTO>));

            List<AgendamentoHorasDTO> lista = (List<AgendamentoHorasDTO>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public void ConfirmarAgendamentoTest_Get()
        {
            // Act
            var result = controller.ConfirmarAgendamento(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ConfirmarAgendamentoDTO));
        }

        [TestMethod()]
        public void ConfirmarAgendamentoTest_Post_Valid()
        {
            // Act
            var result = controller.ConfirmarAgendamento(GetNewAgendarServico());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("AgendamentoConfirmado", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void ConfirmarAgendamentoTest_Post_InValid()
        {
            // Arrange
            controller.ModelState.AddModelError("IdCidadao", "Campo requerido");

            // Act
            var result = controller.ConfirmarAgendamento(GetNewAgendarServico());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
        }

        [TestMethod()]
        public void GetCidadaoTest()
        {
            // Act
            var result = controller.GetCidadao("000.000.000-00");

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CidadaoDTO));
        }

        [TestMethod()]
        public void AgendamentoConfirmadoTest()
        {
            // Act
            var result = controller.AgendamentoConfirmado(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(AgendamentoDTO));
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

        private AgendamentoDTO GetTargetAgendamentoDTO()
        {
            return new AgendamentoDTO
            {
                Id = 1,
                Tipo = "Agendamento",
                Situacao = "Agendado",
                NomeServico = "Clínico Geral",
                OrgaoPublico = "Clínica Dono Mininão",
                Bairro = "Centro",
                Rua = "Rua da Clínica Dono Mininão",
                Numero = "11",
                Complemento = "Sem",
                Data = new DateTime(2022, 10, 09),
                Horario = "07:00 às 12:00",
                DataCadastro = DateTime.Now
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

        private IEnumerable<ServicoPublicoDTO> GetTestServicosPublico()
        {
            return new List<ServicoPublicoDTO>
            {
                new ServicoPublicoDTO
                {
                    Nome = "Clínico Geral",
                    Icone = "qualquer icone",
                },
                new ServicoPublicoDTO
                {
                    Nome = "Clínico Geral",
                    Icone = "qualquer icone",
                },
                new ServicoPublicoDTO
                {
                    Nome = "Clínico Geral",
                    Icone = "qualquer icone",
                }
            };
        }

        private IEnumerable<OrgaoPublicoDTO> GetTestOrgaosPublicos()
        {
            return new List<OrgaoPublicoDTO>
            {
                new OrgaoPublicoDTO
                {
                    Id = 1,
                    Nome = "OAB-Ordem dos Advogados do Brasil de Sergipe",
                    Bairro = "Centro",
                    Rua = "Av. Dr. Luiz Magalhães",
                    Numero = "9",
                    Atendimento = "07:00 às 12:00"
                },
                new OrgaoPublicoDTO
                {
                    Id = 1,
                    Nome = "Hospital Regional de itabaiana Dr. Pedro Garcia Moreno",
                    Bairro = "Centro",
                    Rua = "Av. Treze de Junho",
                    Numero = "776",
                    Atendimento = "07:00 às 12:00"

                },
                new OrgaoPublicoDTO
                {
                    Id = 1,
                    Nome = "Secretaria de Saúde de Itabaiana Sergipe",
                    Bairro = "Sítio Porto",
                    Rua = "Av. Ver. Olímpio Grande",
                    Numero = "77",
                    Atendimento = "07:00 às 12:00"
                }
            };
        }

        private IEnumerable<AgendamentoDiasDTO> GetTestAgendamentoDias()
        {
            return new List<AgendamentoDiasDTO>
            {
                new AgendamentoDiasDTO
                {
                    IdServico = 1,
                    DiaSemana = "Segunda",
                    Data = new DateTime(2022, 10, 09),
                    Vagas = 20
                },
                new AgendamentoDiasDTO
                {
                    IdServico = 1,
                    DiaSemana = "Terça",
                    Data = new DateTime(2022, 10, 09),
                    Vagas = 20

                },
                new AgendamentoDiasDTO
                {
                    IdServico = 1,
                    DiaSemana = "Quarta",
                    Data = new DateTime(2022, 10, 09),
                    Vagas = 20
                }
            };
        }

        private IEnumerable<AgendamentoHorasDTO> GetTestAgendamentoHoras()
        {
            return new List<AgendamentoHorasDTO>
            {
                new AgendamentoHorasDTO
                {
                    Id = 1,
                    IdServico = 1,
                    HorarioInicio = "07:00",
                    HorarioFim = "12:00",
                    Vagas = 10
                },
                new AgendamentoHorasDTO
                {
                    Id = 2,
                    IdServico = 1,
                    HorarioInicio = "13:00",
                    HorarioFim = "17:00",
                    Vagas = 10

                },
                new AgendamentoHorasDTO
                {
                    Id = 1,
                    IdServico = 1,
                    HorarioInicio = "18:00",
                    HorarioFim = "20:00",
                    Vagas = 5
                }
            };
        }

        private ConfirmarAgendamentoDTO GetTargetConfirmarAgendamento()
        {
            return new ConfirmarAgendamentoDTO
            {
                Id = 1,
                NomeServico = "Clínico Geral",
                OrgaoPublico = "Clínica Dono Mininão",
                Bairro = "Centro",
                Rua = "Rua da Clínica Dono Mininão",
                Numero = "111",
                Complemento = "Próximo ao México Maravilhoso",
                Data = new DateTime(2022, 10, 09),
                Horario = "07:00 às 12:00",
                DataCadastro = DateTime.Now
            };
        }

        private CidadaoDTO GetTargetCidadao()
        {
            return new CidadaoDTO
            {
                Id = 2,
                Nome = "José Vinícius de Carvalho Oliveira",
                Cpf = "649.105.050-59",
                Sus = "715291864550008",
                DataNascimento = DateTime.Parse("1979-06-21"),
                Sexo = "M",
                Cep = "49560-000",
                Estado = "SE",
                Cidade = "Moita Bonita",
                Bairro = "Anísio Amâncio de Oliveira",
                Rua = "Rua Ribeiropolis",
                NumeroCasa = "S/N",
                Complemento = "Rua do ginásio",
                Email = "emailteste123@gmail.com",
                Telefone = "79 994429921"
            };
        }
    }
}