using Microsoft.AspNetCore.Mvc;
using SolRural.Models;
using SolRural.Service;

namespace SolRural.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstalacaoController : ControllerBase
    {
        private readonly InstalacaoService _instalacaoService;

        public InstalacaoController(InstalacaoService instalacaoService)
        {
            _instalacaoService = instalacaoService;
        }

        /// <summary>
        /// Retorna todas as instalações.
        /// </summary>
        /// <returns>Lista de instalações</returns>
        /// <response code="200">Retorna a lista de cultivos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Instalacao>> GetInstalacoes() => await _instalacaoService.GetAsync();

        /// <summary>
        /// Retorna uma instalação pelo ID.
        /// </summary>
        /// <param name="id">ID da instalação</param>
        /// <returns>Uma instalação</returns>
        /// <response code="200">Retorna a instalação solicitada</response>
        /// <response code="404">Se a instalação não for encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Instalacao>> GetInstalacao(string id)
        {
            var instalacao = await _instalacaoService.GetAsync(id);

            if (instalacao == null)
            {
                return NotFound();
            }

            return instalacao;
        }

        /// <summary>
        /// Cria uma nova instalação.
        /// </summary>
        /// <param name="instalacao">Dados da nova instalação</param>
        /// <returns>A instalação criada</returns>
        /// <response code="201">Retorna a instalação recém-criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Instalacao> PostInstalacao(Instalacao instalacao)
        {
            await _instalacaoService.CreateAsync(instalacao);

            return instalacao;
        }

        /// <summary>
        /// Atualiza uma instalação existente.
        /// </summary>
        /// <param name="id">ID da instalação</param>
        /// <param name="instalacaoAtualizada">Dados atualizados da instalação</param>
        /// <response code="200">Retorna a instalação atualizada</response>
        /// <response code="404">Se a instalação não for encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateInstalacao(string id, Instalacao instalacaoAtualizada)
        {
            var instalacao = await _instalacaoService.GetAsync(id);

            if (instalacao == null)
            {
                return NotFound("Instalação não encontrada.");
            }

            await _instalacaoService.UpdateAsync(id, instalacaoAtualizada);

            return Ok(instalacao);
        }

        /// <summary>
        /// Deleta uma instalação pelo ID.
        /// </summary>
        /// <param name="id">ID da instalação</param>
        /// <response code="204">Confirma a exclusão da instalação</response>
        /// <response code="404">Se a instalação não for encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteInstalacao(string id)
        {
            await _instalacaoService.RemoveAsync(id);

            return NoContent();
        }
    }
}
