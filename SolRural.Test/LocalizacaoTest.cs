using Microsoft.AspNetCore.Mvc;
using Moq;
using SolRural.Controllers;
using SolRural.Models;
using SolRural.Service;
using Xunit;

namespace SolRural.Test
{
    public class LocalizacaoTest
    {
        private readonly Mock<LocalizacaoService> _mockService;
        private readonly LocalizacaoController _controller;

        public LocalizacaoTest()
        {
            _mockService = new Mock<LocalizacaoService>();
            _controller = new LocalizacaoController(_mockService.Object);
        }

        [Fact]
        public async Task GetLocalizacoes_ReturnsListOfLocalizacoes()
        {
            var localizacoes = new List<Localizacao>
            {
                new Localizacao { Id = "1", Latitude = -5.7945, Longitude = -35.211 },
                new Localizacao { Id = "2", Latitude = -23.5505, Longitude = -46.6333 }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(localizacoes);

            var result = await _controller.GetLocalizacoes();

            Assert.IsType<List<Localizacao>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetLocalizacao_ReturnsLocalizacao_WhenLocalizacaoExists()
        {
            var localizacaoId = "1";
            var localizacao = new Localizacao { Id = localizacaoId, Latitude = -5.7945, Longitude = -35.211 };
            _mockService.Setup(service => service.GetAsync(localizacaoId)).ReturnsAsync(localizacao);

            var result = await _controller.GetLocalizacao(localizacaoId);

            var actionResult = Assert.IsType<ActionResult<Localizacao>>(result);
            var returnValue = Assert.IsType<Localizacao>(actionResult.Value);
            Assert.Equal(localizacaoId, returnValue.Id);
        }

        [Fact]
        public async Task GetLocalizacao_ReturnsNotFound_WhenLocalizacaoDoesNotExist()
        {
            var localizacaoId = "1";
            _mockService.Setup(service => service.GetAsync(localizacaoId)).ReturnsAsync((Localizacao)null);

            var result = await _controller.GetLocalizacao(localizacaoId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostLocalizacao_ReturnsCreatedLocalizacao()
        {
            var localizacao = new Localizacao { Id = "1", Latitude = -5.7945, Longitude = -35.211 };
            _mockService.Setup(service => service.CreateAsync(localizacao)).Returns(Task.CompletedTask);

            var result = await _controller.PostLocalizacao(localizacao);

            Assert.Equal(localizacao, result);
        }

        [Fact]
        public async Task UpdateLocalizacao_ReturnsOk_WhenLocalizacaoExists()
        {
            var localizacaoId = "1";
            var localizacao = new Localizacao { Id = localizacaoId, Latitude = -5.7945, Longitude = -35.211 };
            var localizacaoAtualizada = new Localizacao { Id = localizacaoId, Latitude = -23.5505, Longitude = -46.6333 };

            _mockService.Setup(service => service.GetAsync(localizacaoId)).ReturnsAsync(localizacao);
            _mockService.Setup(service => service.UpdateAsync(localizacaoId, localizacaoAtualizada)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateLocalizacao(localizacaoId, localizacaoAtualizada);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateLocalizacao_ReturnsNotFound_WhenLocalizacaoDoesNotExist()
        {
            var localizacaoId = "1";
            var localizacaoAtualizada = new Localizacao { Id = localizacaoId, Latitude = -23.5505, Longitude = -46.6333 };

            _mockService.Setup(service => service.GetAsync(localizacaoId)).ReturnsAsync((Localizacao)null);

            var result = await _controller.UpdateLocalizacao(localizacaoId, localizacaoAtualizada);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Localização não encontrada.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteLocalizacao_ReturnsNoContent_WhenLocalizacaoIsDeleted()
        {
            var localizacaoId = "1";
            _mockService.Setup(service => service.RemoveAsync(localizacaoId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteLocalizacao(localizacaoId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
