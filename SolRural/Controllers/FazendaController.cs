using Microsoft.AspNetCore.Mvc;
using SolRural.Models;
using SolRural.Service;

namespace SolRural.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FazendaController : ControllerBase
    {
        private readonly FazendaService _fazendaService;

        public FazendaController(FazendaService fazendaService)
        {
            _fazendaService = fazendaService;
        }

        /// <summary>
        /// Retorna todas as fazendas.
        /// </summary>
        /// <returns>Lista de fazendas</returns>
        /// <response code="200">Retorna a lista de fazendas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Fazenda>> GetFazendas() => await _fazendaService.GetAsync();

        /// <summary>
        /// Retorna uma fazenda pelo ID.
        /// </summary>
        /// <param name="id">ID da fazenda</param>
        /// <returns>Uma fazenda</returns>
        /// <response code="200">Retorna a fazenda solicitada</response>
        /// <response code="404">Se a fazenda não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Fazenda>> GetFazenda(string id)
        {
            var fazenda = await _fazendaService.GetAsync(id);

            if (fazenda == null)
            {
                return NotFound();
            }

            return fazenda;
        }

        /// <summary>
        /// Cria uma nova fazenda.
        /// </summary>
        /// <param name="fazenda">Dados da nova fazenda</param>
        /// <returns>A fazenda criada</returns>
        /// <response code="201">Retorna a fazenda recém-criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Fazenda> PostFazenda(Fazenda fazenda)
        {
            await _fazendaService.CreateAsync(fazenda);

            return fazenda;
        }

        /// <summary>
        /// Atualiza uma fazenda existente.
        /// </summary>
        /// <param name="id">ID da fazenda</param>
        /// <param name="fazendaAtualizada">Dados atualizados da fazenda</param>
        /// <response code="200">Retorna a fazenda atualizada</response>
        /// <response code="404">Se a fazenda não for encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateFazenda(string id, Fazenda fazendaAtualizada)
        {
            var fazenda = await _fazendaService.GetAsync(id);

            if (fazenda == null)
            {
                return NotFound("Fazenda não encontrada.");
            }

            await _fazendaService.UpdateAsync(id, fazendaAtualizada);

            return Ok(fazenda);
        }

        /// <summary>
        /// Deleta uma fazenda pelo ID.
        /// </summary>
        /// <param name="id">ID da fazenda</param>
        /// <response code="204">Confirma a exclusão da fazenda</response>
        /// <response code="404">Se a fazenda não for encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFazenda(string id)
        {
            await _fazendaService.RemoveAsync(id);

            return NoContent();
        }
    }
}
