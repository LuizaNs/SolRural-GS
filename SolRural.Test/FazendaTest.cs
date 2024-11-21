using Microsoft.AspNetCore.Mvc;
using Moq;
using SolRural.Controllers;
using SolRural.Models;
using SolRural.Service;
using Xunit;

namespace SolRural.Test
{
    public class FazendaTest
    {
        private readonly Mock<FazendaService> _mockService;
        private readonly FazendaController _controller;

        public FazendaTest()
        {
            _mockService = new Mock<FazendaService>();
            _controller = new FazendaController(_mockService.Object);
        }

        [Fact]
        public async Task GetFazendas_ReturnsListOfFazendas()
        {
            var fazendas = new List<Fazenda>
            {
                new Fazenda { Id = "1", Area = 50.5 },
                new Fazenda { Id = "2", Area = 120.3 }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(fazendas);

            var result = await _controller.GetFazendas();

            Assert.IsType<List<Fazenda>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetFazenda_ReturnsFazenda_WhenFazendaExists()
        {
            var fazendaId = "1";
            var fazenda = new Fazenda { Id = fazendaId, Area = 50.5 };
            _mockService.Setup(service => service.GetAsync(fazendaId)).ReturnsAsync(fazenda);

            var result = await _controller.GetFazenda(fazendaId);

            var actionResult = Assert.IsType<ActionResult<Fazenda>>(result);
            var returnValue = Assert.IsType<Fazenda>(actionResult.Value);
            Assert.Equal(fazendaId, returnValue.Id);
        }

        [Fact]
        public async Task GetFazenda_ReturnsNotFound_WhenFazendaDoesNotExist()
        {
            var fazendaId = "1";
            _mockService.Setup(service => service.GetAsync(fazendaId)).ReturnsAsync((Fazenda)null);

            var result = await _controller.GetFazenda(fazendaId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostFazenda_ReturnsCreatedFazenda()
        {
            var fazenda = new Fazenda { Id = "1", Area = 50.5 };
            _mockService.Setup(service => service.CreateAsync(fazenda)).Returns(Task.CompletedTask);

            var result = await _controller.PostFazenda(fazenda);

            Assert.Equal(fazenda, result);
        }

        [Fact]
        public async Task UpdateFazenda_ReturnsOk_WhenFazendaExists()
        {
            var fazendaId = "1";
            var fazenda = new Fazenda { Id = fazendaId, Area = 50.5 };
            var fazendaAtualizada = new Fazenda { Id = fazendaId, Area = 75.0 };

            _mockService.Setup(service => service.GetAsync(fazendaId)).ReturnsAsync(fazenda);
            _mockService.Setup(service => service.UpdateAsync(fazendaId, fazendaAtualizada)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateFazenda(fazendaId, fazendaAtualizada);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateFazenda_ReturnsNotFound_WhenFazendaDoesNotExist()
        {
            var fazendaId = "1";
            var fazendaAtualizada = new Fazenda { Id = fazendaId, Area = 75.0 };

            _mockService.Setup(service => service.GetAsync(fazendaId)).ReturnsAsync((Fazenda)null);

            var result = await _controller.UpdateFazenda(fazendaId, fazendaAtualizada);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Fazenda não encontrada.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteFazenda_ReturnsNoContent_WhenFazendaIsDeleted()
        {
            var fazendaId = "1";
            _mockService.Setup(service => service.RemoveAsync(fazendaId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteFazenda(fazendaId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
