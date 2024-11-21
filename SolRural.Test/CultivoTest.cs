using Microsoft.AspNetCore.Mvc;
using Moq;
using SolRural.Controllers;
using SolRural.Models;
using SolRural.Service;
using Xunit;

namespace SolRural.Test
{
    public class CultivoTest
    {
        private readonly Mock<CultivoService> _mockService;
        private readonly CultivoController _controller;

        public CultivoTest()
        {
            _mockService = new Mock<CultivoService>();
            _controller = new CultivoController(_mockService.Object);
        }

        [Fact]
        public async Task GetCultivos_ReturnsListOfCultivos()
        {
            var cultivos = new List<Cultivo>
            {
                new Cultivo
                {
                    Id = "1",
                    TipoCultivo = "Milho",
                    Sazonalidade = "Verão",
                    AreaOcupada = 15.5
                }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(cultivos);

            var result = await _controller.GetCultivos();

            Assert.IsType<List<Cultivo>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetCultivo_ReturnsCultivo_WhenCultivoExists()
        {
            var cultivoId = "1";
            var cultivo = new Cultivo
            {
                Id = cultivoId,
                TipoCultivo = "Milho",
                Sazonalidade = "Verão",
                AreaOcupada = 15.5
            };
            _mockService.Setup(service => service.GetAsync(cultivoId)).ReturnsAsync(cultivo);

            var result = await _controller.GetCultivo(cultivoId);

            var actionResult = Assert.IsType<ActionResult<Cultivo>>(result);
            var returnValue = Assert.IsType<Cultivo>(actionResult.Value);
            Assert.Equal(cultivoId, returnValue.Id);
        }

        [Fact]
        public async Task GetCultivo_ReturnsNotFound_WhenCultivoDoesNotExist()
        {
            var cultivoId = "1";
            _mockService.Setup(service => service.GetAsync(cultivoId)).ReturnsAsync((Cultivo)null);

            var result = await _controller.GetCultivo(cultivoId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostCultivo_ReturnsCreatedCultivo()
        {
            var cultivo = new Cultivo
            {
                Id = "1",
                TipoCultivo = "Milho",
                Sazonalidade = "Verão",
                AreaOcupada = 15.5
            };
            _mockService.Setup(service => service.CreateAsync(cultivo)).Returns(Task.CompletedTask);

            var result = await _controller.PostCultivo(cultivo);

            Assert.Equal(cultivo, result);
        }

        [Fact]
        public async Task UpdateCultivo_ReturnsOk_WhenCultivoExists()
        {
            var cultivoId = "1";
            var cultivo = new Cultivo
            {
                Id = cultivoId,
                TipoCultivo = "Milho",
                Sazonalidade = "Verão",
                AreaOcupada = 15.5
            };
            var cultivoAtualizado = new Cultivo
            {
                Id = cultivoId,
                TipoCultivo = "Soja",
                Sazonalidade = "Inverno",
                AreaOcupada = 20.0
            };

            _mockService.Setup(service => service.GetAsync(cultivoId)).ReturnsAsync(cultivo);
            _mockService.Setup(service => service.UpdateAsync(cultivoId, cultivoAtualizado)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateCultivo(cultivoId, cultivoAtualizado);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCultivo_ReturnsNotFound_WhenCultivoDoesNotExist()
        {
            var cultivoId = "1";
            var cultivoAtualizado = new Cultivo
            {
                Id = cultivoId,
                TipoCultivo = "Soja",
                Sazonalidade = "Inverno",
                AreaOcupada = 20.0
            };

            _mockService.Setup(service => service.GetAsync(cultivoId)).ReturnsAsync((Cultivo)null);

            var result = await _controller.UpdateCultivo(cultivoId, cultivoAtualizado);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Cultivo não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteCultivo_ReturnsNoContent_WhenCultivoIsDeleted()
        {
            var cultivoId = "1";
            _mockService.Setup(service => service.RemoveAsync(cultivoId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteCultivo(cultivoId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
            
