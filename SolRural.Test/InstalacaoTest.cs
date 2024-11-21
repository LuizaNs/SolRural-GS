using Microsoft.AspNetCore.Mvc;
using Moq;
using SolRural.Controllers;
using SolRural.Models;
using SolRural.Service;
using Xunit;

namespace SolRural.Test
{
    public class InstalacaoTest
    {
        private readonly Mock<InstalacaoService> _mockService;
        private readonly InstalacaoController _controller;

        public InstalacaoTest()
        {
            _mockService = new Mock<InstalacaoService>();
            _controller = new InstalacaoController(_mockService.Object);
        }

        [Fact]
        public async Task GetInstalacoes_ReturnsListOfInstalacoes()
        {
            var instalacoes = new List<Instalacao>
            {
                new Instalacao { Id = "1", DataInstalacao = new DateOnly(2024, 1, 1), Status = "Ativa" },
                new Instalacao { Id = "2", DataInstalacao = new DateOnly(2023, 12, 15), Status = "Pendente" }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(instalacoes);

            var result = await _controller.GetInstalacoes();

            Assert.IsType<List<Instalacao>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetInstalacao_ReturnsInstalacao_WhenInstalacaoExists()
        {
            var instalacaoId = "1";
            var instalacao = new Instalacao { Id = instalacaoId, DataInstalacao = new DateOnly(2024, 1, 1), Status = "Ativa" };
            _mockService.Setup(service => service.GetAsync(instalacaoId)).ReturnsAsync(instalacao);

            var result = await _controller.GetInstalacao(instalacaoId);

            var actionResult = Assert.IsType<ActionResult<Instalacao>>(result);
            var returnValue = Assert.IsType<Instalacao>(actionResult.Value);
            Assert.Equal(instalacaoId, returnValue.Id);
        }

        [Fact]
        public async Task GetInstalacao_ReturnsNotFound_WhenInstalacaoDoesNotExist()
        {
            var instalacaoId = "1";
            _mockService.Setup(service => service.GetAsync(instalacaoId)).ReturnsAsync((Instalacao)null);

            var result = await _controller.GetInstalacao(instalacaoId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostInstalacao_ReturnsCreatedInstalacao()
        {
            var instalacao = new Instalacao { Id = "1", DataInstalacao = new DateOnly(2024, 1, 1), Status = "Ativa" };
            _mockService.Setup(service => service.CreateAsync(instalacao)).Returns(Task.CompletedTask);

            var result = await _controller.PostInstalacao(instalacao);

            Assert.Equal(instalacao, result);
        }

        [Fact]
        public async Task UpdateInstalacao_ReturnsOk_WhenInstalacaoExists()
        {
            var instalacaoId = "1";
            var instalacao = new Instalacao { Id = instalacaoId, DataInstalacao = new DateOnly(2024, 1, 1), Status = "Ativa" };
            var instalacaoAtualizada = new Instalacao { Id = instalacaoId, DataInstalacao = new DateOnly(2024, 2, 1), Status = "Concluída" };

            _mockService.Setup(service => service.GetAsync(instalacaoId)).ReturnsAsync(instalacao);
            _mockService.Setup(service => service.UpdateAsync(instalacaoId, instalacaoAtualizada)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateInstalacao(instalacaoId, instalacaoAtualizada);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateInstalacao_ReturnsNotFound_WhenInstalacaoDoesNotExist()
        {
            var instalacaoId = "1";
            var instalacaoAtualizada = new Instalacao { Id = instalacaoId, DataInstalacao = new DateOnly(2024, 2, 1), Status = "Concluída" };

            _mockService.Setup(service => service.GetAsync(instalacaoId)).ReturnsAsync((Instalacao)null);

            var result = await _controller.UpdateInstalacao(instalacaoId, instalacaoAtualizada);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Instalação não encontrada.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteInstalacao_ReturnsNoContent_WhenInstalacaoIsDeleted()
        {
            var instalacaoId = "1";
            _mockService.Setup(service => service.RemoveAsync(instalacaoId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteInstalacao(instalacaoId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
