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
    public class CidadaoControllerTests
    {
        private static CidadaoController controller;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<ICidadaoService>();

            IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new CidadaoProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll()).Returns(GetTestCidadaos);
            mockService.Setup(service => service.Get(1))
                .Returns(GetTargetCidadao());
            mockService.Setup(service => service.Edit(It.IsAny<Cidadao>()))
                .Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Cidadao>()))
                .Verifiable();
            controller = new CidadaoController(mockService.Object, mapper);
        }

        [TestMethod()]
        public void IndexTest()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<CidadaoViewModel>));

            List<CidadaoViewModel>? lista = (List<CidadaoViewModel>)viewResult.ViewData.Model;
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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CidadaoViewModel));
            CidadaoViewModel cidadaoViewModel = (CidadaoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("José Vinícius de Carvalho Oliveira", cidadaoViewModel.Nome);
            Assert.AreEqual("649.105.050-59", cidadaoViewModel.Cpf);
            Assert.AreEqual("715291864550008", cidadaoViewModel.Sus);
            Assert.AreEqual(DateTime.Parse("1979-06-21"), cidadaoViewModel.DataNascimento);
            Assert.AreEqual("M", cidadaoViewModel.Sexo);
            Assert.AreEqual("49560-000", cidadaoViewModel.Cep);
            Assert.AreEqual("SE", cidadaoViewModel.Estado);
            Assert.AreEqual("Moita Bonita", cidadaoViewModel.Cidade);
            Assert.AreEqual("Anísio Amâncio de Oliveira", cidadaoViewModel.Bairro);
            Assert.AreEqual("Rua Ribeiropolis", cidadaoViewModel.Rua);
            Assert.AreEqual("S/N", cidadaoViewModel.NumeroCasa);
            Assert.AreEqual("Rua do ginásio", cidadaoViewModel.Complemento);
            Assert.AreEqual("emailteste123@gmail.com", cidadaoViewModel.Email);
            Assert.AreEqual("79 994429921", cidadaoViewModel.Telefone);
            Assert.AreEqual("Profissional", cidadaoViewModel.TipoCidadao);
            Assert.AreEqual(1, cidadaoViewModel.IdOrgaoPublico);
            Assert.AreEqual(1, cidadaoViewModel.IdPrefeitura);
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
            var result = controller.Create(GetNewCidadao());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void CreateTest_Invalid()
        {
            // Arrange
            controller.ModelState.AddModelError("Nome", "Campo requerido");

            // Act
            var result = controller.Create(GetNewCidadao());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void EditTest_Get()
        {
            var result = controller.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CidadaoViewModel));
            CidadaoViewModel cidadaoViewModel = (CidadaoViewModel)viewResult.ViewData.Model;

            Assert.AreEqual("José Vinícius de Carvalho Oliveira", cidadaoViewModel.Nome);
            Assert.AreEqual("649.105.050-59", cidadaoViewModel.Cpf);
            Assert.AreEqual("715291864550008", cidadaoViewModel.Sus);
            Assert.AreEqual(DateTime.Parse("1979-06-21"), cidadaoViewModel.DataNascimento);
            Assert.AreEqual("M", cidadaoViewModel.Sexo);
            Assert.AreEqual("49560-000", cidadaoViewModel.Cep);
            Assert.AreEqual("SE", cidadaoViewModel.Estado);
            Assert.AreEqual("Moita Bonita", cidadaoViewModel.Cidade);
            Assert.AreEqual("Anísio Amâncio de Oliveira", cidadaoViewModel.Bairro);
            Assert.AreEqual("Rua Ribeiropolis", cidadaoViewModel.Rua);
            Assert.AreEqual("S/N", cidadaoViewModel.NumeroCasa);
            Assert.AreEqual("Rua do ginásio", cidadaoViewModel.Complemento);
            Assert.AreEqual("emailteste123@gmail.com", cidadaoViewModel.Email);
            Assert.AreEqual("79 994429921", cidadaoViewModel.Telefone);
            Assert.AreEqual("Profissional", cidadaoViewModel.TipoCidadao);
            Assert.AreEqual(1, cidadaoViewModel.IdOrgaoPublico);
            Assert.AreEqual(1, cidadaoViewModel.IdPrefeitura);
        }

        [TestMethod()]
        public void EditTest_Post()
        {
            // Act
            var result = controller.Edit(GetTargetCidadaoViewModel().Id, GetTargetCidadaoViewModel());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CidadaoViewModel));
            CidadaoViewModel cidadaoViewModel = (CidadaoViewModel)viewResult.ViewData.Model;

            Assert.AreEqual("José Vinícius de Carvalho Oliveira", cidadaoViewModel.Nome);
            Assert.AreEqual("649.105.050-59", cidadaoViewModel.Cpf);
            Assert.AreEqual("715291864550008", cidadaoViewModel.Sus);
            Assert.AreEqual(DateTime.Parse("1979-06-21"), cidadaoViewModel.DataNascimento);
            Assert.AreEqual("M", cidadaoViewModel.Sexo);
            Assert.AreEqual("49560-000", cidadaoViewModel.Cep);
            Assert.AreEqual("SE", cidadaoViewModel.Estado);
            Assert.AreEqual("Moita Bonita", cidadaoViewModel.Cidade);
            Assert.AreEqual("Anísio Amâncio de Oliveira", cidadaoViewModel.Bairro);
            Assert.AreEqual("Rua Ribeiropolis", cidadaoViewModel.Rua);
            Assert.AreEqual("S/N", cidadaoViewModel.NumeroCasa);
            Assert.AreEqual("Rua do ginásio", cidadaoViewModel.Complemento);
            Assert.AreEqual("emailteste123@gmail.com", cidadaoViewModel.Email);
            Assert.AreEqual("79 994429921", cidadaoViewModel.Telefone);
            Assert.AreEqual("Profissional", cidadaoViewModel.TipoCidadao);
            Assert.AreEqual(1, cidadaoViewModel.IdOrgaoPublico);
            Assert.AreEqual(1, cidadaoViewModel.IdPrefeitura);
        }

        [TestMethod()]
        public void DeleteTest_Get()
        {
            // Act
            var result = controller.Delete(GetTargetCidadaoViewModel().Id, GetTargetCidadaoViewModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }
        
        private CidadaoViewModel GetNewCidadao()
        {
            return new CidadaoViewModel
            {
                Id = 3,
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
                Telefone = "79 994429921",
                TipoCidadao = "Profissional",
                IdOrgaoPublico = 1,
                IdPrefeitura = 1
            };
        }

        private CidadaoViewModel GetTargetCidadaoViewModel()
        {
            return new CidadaoViewModel
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
                Telefone = "79 994429921",
                TipoCidadao = "Profissional",
                IdOrgaoPublico = 1,
                IdPrefeitura = 1
            };
        }

        private Cidadao GetTargetCidadao()
        {
            return new Cidadao
            {
                Id = 1,
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
                Telefone = "79 994429921",
                TipoCidadao = "Profissional",
                IdOrgaoPublico = 1,
                IdPrefeitura = 1
            };
        }
        private IEnumerable<Cidadao> GetTestCidadaos()
        {
            return new List<Cidadao>
            {
                new Cidadao
                {
                    Id = 1,
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
                    Telefone = "79 994429921",
                    TipoCidadao = "Profissional",
                    IdOrgaoPublico = 1,
                    IdPrefeitura = 1
                },
                new Cidadao
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
                    Telefone = "79 994429921",
                    TipoCidadao = "Profissional",
                    IdOrgaoPublico = 1,
                    IdPrefeitura = 1
                }
            };
        }
    }
}