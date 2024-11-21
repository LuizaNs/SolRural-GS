using Microsoft.AspNetCore.Mvc;
using SolRural.Models;
using SolRural.Service;

namespace SolRural.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _localizacaoService;

        public LocalizacaoController(LocalizacaoService localizacaoService)
        {
            _localizacaoService = localizacaoService;
        }

        /// <summary>
        /// Retorna todas as localizações.
        /// </summary>
        /// <returns>Lista de localizações</returns>
        /// <response code="200">Retorna a lista de localizações</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Localizacao>> GetLocalizacoes() => await _localizacaoService.GetAsync();

        /// <summary>
        /// Retorna uma localização pelo ID.
        /// </summary>
        /// <param name="id">ID da localização</param>
        /// <returns>Uma localização</returns>
        /// <response code="200">Retorna a localização solicitada</response>
        /// <response code="404">Se a localização não for encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Localizacao>> GetLocalizacao(string id)
        {
            var localizacao = await _localizacaoService.GetAsync(id);

            if (localizacao == null)
            {
                return NotFound();
            }

            return localizacao;
        }

        /// <summary>
        /// Cria uma nova localização.
        /// </summary>
        /// <param name="localizacao">Dados da nova localização</param>
        /// <returns>A localização criada</returns>
        /// <response code="201">Retorna a localização recém-criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Localizacao> PostLocalizacao(Localizacao localizacao)
        {
            await _localizacaoService.CreateAsync(localizacao);

            return localizacao;
        }

        /// <summary>
        /// Atualiza uma localização existente.
        /// </summary>
        /// <param name="id">ID da localização</param>
        /// <param name="localizacaoAtualizada">Dados atualizados da localização</param>
        /// <response code="200">Retorna a localização atualizada</response>
        /// <response code="404">Se a localização não for encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLocalizacao(string id, Localizacao localizacaoAtualizada)
        {
            var localizacao = await _localizacaoService.GetAsync(id);

            if (localizacao == null)
            {
                return NotFound("Localização não encontrada.");
            }

            await _localizacaoService.UpdateAsync(id, localizacaoAtualizada);

            return Ok(localizacao);
        }

        /// <summary>
        /// Deleta uma localização pelo ID.
        /// </summary>
        /// <param name="id">ID da localização</param>
        /// <response code="204">Confirma a exclusão da localização</response>
        /// <response code="404">Se a localização não for encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLocalizacao(string id)
        {
            await _localizacaoService.RemoveAsync(id);

            return NoContent();
        }
    }
}
