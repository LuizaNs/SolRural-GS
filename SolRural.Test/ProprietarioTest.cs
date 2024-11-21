using Microsoft.AspNetCore.Mvc;
using Moq;
using SolRural.Controllers;
using SolRural.Models;
using SolRural.Service;
using Xunit;

namespace SolRural.Test
{
    public class ProprietarioTest
    {
        private readonly Mock<ProprietarioService> _mockService;
        private readonly ProprietarioController _controller;

        public ProprietarioTest()
        {
            _mockService = new Mock<ProprietarioService>();
            _controller = new ProprietarioController(_mockService.Object);
        }

        [Fact]
        public async Task GetProprietarios_ReturnsListOfProprietarios()
        {
            var proprietarios = new List<Proprietario>
            {
                new Proprietario { Id = "1", Nome = "João Silva", Telefone = 123456789, Email = "joao@example.com" },
                new Proprietario { Id = "2", Nome = "Maria Oliveira", Telefone = 987654321, Email = "maria@example.com" }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(proprietarios);

            var result = await _controller.GetProprietarios();

            Assert.IsType<List<Proprietario>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetProprietario_ReturnsProprietario_WhenProprietarioExists()
        {
            var proprietarioId = "1";
            var proprietario = new Proprietario { Id = proprietarioId, Nome = "João Silva", Telefone = 123456789, Email = "joao@example.com" };
            _mockService.Setup(service => service.GetAsync(proprietarioId)).ReturnsAsync(proprietario);

            var result = await _controller.GetProprietario(proprietarioId);

            var actionResult = Assert.IsType<ActionResult<Proprietario>>(result);
            var returnValue = Assert.IsType<Proprietario>(actionResult.Value);
            Assert.Equal(proprietarioId, returnValue.Id);
        }

        [Fact]
        public async Task GetProprietario_ReturnsNotFound_WhenProprietarioDoesNotExist()
        {
            var proprietarioId = "1";
            _mockService.Setup(service => service.GetAsync(proprietarioId)).ReturnsAsync((Proprietario)null);

            var result = await _controller.GetProprietario(proprietarioId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostProprietario_ReturnsCreatedProprietario()
        {
            var proprietario = new Proprietario { Id = "1", Nome = "João Silva", Telefone = 123456789, Email = "joao@example.com" };
            _mockService.Setup(service => service.CreateAsync(proprietario)).Returns(Task.CompletedTask);

            var result = await _controller.PostProprietario(proprietario);

            Assert.Equal(proprietario, result);
        }

        [Fact]
        public async Task UpdateProprietario_ReturnsOk_WhenProprietarioExists()
        {
            var proprietarioId = "1";
            var proprietario = new Proprietario { Id = proprietarioId, Nome = "João Silva", Telefone = 123456789, Email = "joao@example.com" };
            var proprietarioAtualizado = new Proprietario { Id = proprietarioId, Nome = "João Atualizado", Telefone = 987654321, Email = "joao.atualizado@example.com" };

            _mockService.Setup(service => service.GetAsync(proprietarioId)).ReturnsAsync(proprietario);
            _mockService.Setup(service => service.UpdateAsync(proprietarioId, proprietarioAtualizado)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateProprietario(proprietarioId, proprietarioAtualizado);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProprietario_ReturnsNotFound_WhenProprietarioDoesNotExist()
        {
            var proprietarioId = "1";
            var proprietarioAtualizado = new Proprietario { Id = proprietarioId, Nome = "João Atualizado", Telefone = 987654321, Email = "joao.atualizado@example.com" };

            _mockService.Setup(service => service.GetAsync(proprietarioId)).ReturnsAsync((Proprietario)null);

            var result = await _controller.UpdateProprietario(proprietarioId, proprietarioAtualizado);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Proprietário não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteProprietario_ReturnsNoContent_WhenProprietarioIsDeleted()
        {
            var proprietarioId = "1";
            _mockService.Setup(service => service.RemoveAsync(proprietarioId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteProprietario(proprietarioId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
