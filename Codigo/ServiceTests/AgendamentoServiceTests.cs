using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass()]
    public class AgendamentoServiceTests
    {
        private AgendeMeContext _context;
        private IAgendamentoService _agendamentoService;

        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            var builder = new DbContextOptionsBuilder<AgendeMeContext>();
            builder.UseInMemoryDatabase("AgendeMe");
            builder.ConfigureWarnings(builder =>
            {
                builder.Ignore(InMemoryEventId.TransactionIgnoredWarning);
            });
            var options = builder.Options;

            _context = new AgendeMeContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var agendamentos = new List<Agendamento>
            {
                new Agendamento {Id = 1, Tipo = "Agendamento", Situacao = "Agendado",  DataCadastro = new DateTime(2023, 02, 20), IdCidadao = 2, IdDiaAgendamento = 4, IdAtendente = null, IdRetorno = null},
                new Agendamento {Id = 2, Tipo = "Retorno", Situacao = "Cancelado", DataCadastro = new DateTime(2023, 02, 27), IdCidadao = 2, IdDiaAgendamento = 7, IdAtendente = null, IdRetorno = 1},
                new Agendamento {Id = 3, Tipo = "Agendamento", Situacao = "Aguardando Atendimento",  DataCadastro = new DateTime(2023, 03, 01), IdCidadao = 3, IdDiaAgendamento = 5, IdAtendente = null, IdRetorno = null},
                new Agendamento {Id = 4, Tipo = "Agendamento", Situacao = "Atendido",  DataCadastro = new DateTime(2023, 02, 27), IdCidadao = 5, IdDiaAgendamento = 7, IdAtendente = null, IdRetorno = null}
            };

            _context.AddRange(agendamentos);
            _context.SaveChanges();

            _agendamentoService = new AgendamentoService(_context);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _agendamentoService.Create(new Agendamento { Id = 5, DataCadastro = new DateTime(2023, 02, 01), IdCidadao = 5, IdDiaAgendamento = 7, IdAtendente = null, IdRetorno = null });

            // Assert
            var agendamento = _agendamentoService.Get(5);
            Assert.AreEqual("Agendado", agendamento.Situacao);
            Assert.AreEqual(5, agendamento.IdCidadao);
            Assert.AreEqual(new DateTime(2023, 02, 01), agendamento.DataCadastro);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            _agendamentoService.Delete(2);

            // Assert
            Assert.AreEqual(3, _agendamentoService.GetAll().Count());
            var agendamento = _agendamentoService.Get(2);
            Assert.AreEqual(null, agendamento);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Act 
            var agendamento = _agendamentoService.Get(1);
            agendamento.Situacao = "Aguardando Atendimento";
            _agendamentoService.Edit(agendamento);

            //Assert
            agendamento = _agendamentoService.Get(1);
            Assert.IsNotNull(agendamento);
            Assert.AreEqual("Aguardando Atendimento", agendamento.Situacao);
        }

        [TestMethod()]
        public void GetTest()
        {
            var agendamento = _agendamentoService.Get(1);
            Assert.IsNotNull(agendamento);
            Assert.AreEqual("Agendado", agendamento.Situacao);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var listaAgendamento = _agendamentoService.GetAll();

            // Assert
            Assert.IsInstanceOfType(listaAgendamento, typeof(IEnumerable<Agendamento>));
            Assert.IsNotNull(listaAgendamento);
            Assert.AreEqual(4, listaAgendamento.Count());
            Assert.AreEqual(1, listaAgendamento.First().Id);
            Assert.AreEqual("Agendado", listaAgendamento.First().Situacao);
        }
    }
}