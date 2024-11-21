using Microsoft.AspNetCore.Mvc;
using Moq;
using SolRural.Controllers;
using SolRural.Models;
using SolRural.Service;
using Xunit;

namespace SolRural.Test
{
    public class MedicaoEnergTest
    {
        private readonly Mock<MedicaoEnergService> _mockService;
        private readonly MedicaoEnergController _controller;

        public MedicaoEnergTest()
        {
            _mockService = new Mock<MedicaoEnergService>();
            _controller = new MedicaoEnergController(_mockService.Object);
        }

        [Fact]
        public async Task GetMedicoes_ReturnsListOfMedicoes()
        {
            var medicoes = new List<MedicaoEnerg>
            {
                new MedicaoEnerg { Id = "1", DataMedicao = new DateOnly(2024, 11, 01), EnergGerada = 100, EnergConsumida = 80, EnergExcedida = 20 },
                new MedicaoEnerg { Id = "2", DataMedicao = new DateOnly(2024, 11, 02), EnergGerada = 120, EnergConsumida = 90, EnergExcedida = 30 }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(medicoes);

            var result = await _controller.GetMedicoesEnerg();

            Assert.IsType<List<MedicaoEnerg>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetMedicao_ReturnsMedicao_WhenMedicaoExists()
        {
            var medicaoId = "1";
            var medicao = new MedicaoEnerg { Id = medicaoId, DataMedicao = new DateOnly(2024, 11, 01), EnergGerada = 100, EnergConsumida = 80, EnergExcedida = 20 };
            _mockService.Setup(service => service.GetAsync(medicaoId)).ReturnsAsync(medicao);

            var result = await _controller.GetMedicaoEnerg(medicaoId);

            var actionResult = Assert.IsType<ActionResult<MedicaoEnerg>>(result);
            var returnValue = Assert.IsType<MedicaoEnerg>(actionResult.Value);
            Assert.Equal(medicaoId, returnValue.Id);
        }

        [Fact]
        public async Task GetMedicao_ReturnsNotFound_WhenMedicaoDoesNotExist()
        {
            var medicaoId = "1";
            _mockService.Setup(service => service.GetAsync(medicaoId)).ReturnsAsync((MedicaoEnerg)null);

            var result = await _controller.GetMedicaoEnerg(medicaoId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostMedicao_ReturnsCreatedMedicao()
        {
            var medicao = new MedicaoEnerg { Id = "1", DataMedicao = new DateOnly(2024, 11, 01), EnergGerada = 100, EnergConsumida = 80, EnergExcedida = 20 };
            _mockService.Setup(service => service.CreateAsync(medicao)).Returns(Task.CompletedTask);

            var result = await _controller.PostMedicaoEnerg(medicao);

            Assert.Equal(medicao, result);
        }

        [Fact]
        public async Task UpdateMedicao_ReturnsOk_WhenMedicaoExists()
        {
            var medicaoId = "1";
            var medicao = new MedicaoEnerg { Id = medicaoId, DataMedicao = new DateOnly(2024, 11, 01), EnergGerada = 100, EnergConsumida = 80, EnergExcedida = 20 };
            var medicaoAtualizada = new MedicaoEnerg { Id = medicaoId, DataMedicao = new DateOnly(2024, 11, 02), EnergGerada = 120, EnergConsumida = 90, EnergExcedida = 30 };

            _mockService.Setup(service => service.GetAsync(medicaoId)).ReturnsAsync(medicao);
            _mockService.Setup(service => service.UpdateAsync(medicaoId, medicaoAtualizada)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateMedicaoEnerg(medicaoId, medicaoAtualizada);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateMedicao_ReturnsNotFound_WhenMedicaoDoesNotExist()
        {
            var medicaoId = "1";
            var medicaoAtualizada = new MedicaoEnerg { Id = medicaoId, DataMedicao = new DateOnly(2024, 11, 02), EnergGerada = 120, EnergConsumida = 90, EnergExcedida = 30 };

            _mockService.Setup(service => service.GetAsync(medicaoId)).ReturnsAsync((MedicaoEnerg)null);

            var result = await _controller.UpdateMedicaoEnerg(medicaoId, medicaoAtualizada);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Medição não encontrada.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteMedicao_ReturnsNoContent_WhenMedicaoIsDeleted()
        {
            var medicaoId = "1";
            _mockService.Setup(service => service.RemoveAsync(medicaoId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteMedicaoEnerg(medicaoId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
