using Microsoft.AspNetCore.Mvc;
using SolRural.Models;
using SolRural.Service;

namespace SolRural.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CultivoController : ControllerBase
    {
        private readonly CultivoService _cultivoService;

        public CultivoController(CultivoService cultivoService)
        {
            _cultivoService = cultivoService;
        }

        /// <summary>
        /// Retorna todos os cultivos.
        /// </summary>
        /// <returns>Lista de cultivos</returns>
        /// <response code="200">Retorna a lista de cultivos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Cultivo>> GetCultivos() => await _cultivoService.GetAsync();

        /// <summary>
        /// Retorna um cultivo pelo ID.
        /// </summary>
        /// <param name="id">ID do cultivo</param>
        /// <returns>Um cultivo</returns>
        /// <response code="200">Retorna o cultivo solicitado</response>
        /// <response code="404">Se o cultivo não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cultivo>> GetCultivo(string id)
        {
            var cultivo = await _cultivoService.GetAsync(id);

            if (cultivo == null)
            {
                return NotFound();
            }

            return cultivo;
        }

        /// <summary>
        /// Cria um novo cultivo.
        /// </summary>
        /// <param name="cultivo">Dados do novo cultivo</param>
        /// <returns>O cultivo criado</returns>
        /// <response code="201">Retorna o cultivo recém-criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Cultivo> PostCultivo(Cultivo cultivo)
        {
            await _cultivoService.CreateAsync(cultivo);

            return cultivo;
        }

        /// <summary>
        /// Atualiza um cultivo existente.
        /// </summary>
        /// <param name="id">ID do cultivo</param>
        /// <param name="cultivoAtualizado">Dados atualizados do cultivo</param>
        /// <response code="200">Retorna o cultivo atualizado</response>
        /// <response code="404">Se o cultivo não for encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCultivo(string id, Cultivo cultivoAtualizado)
        {
            var cultivo = await _cultivoService.GetAsync(id);

            if (cultivo == null)
            {
                return NotFound("Cultivo não encontrado.");
            }

            await _cultivoService.UpdateAsync(id, cultivoAtualizado);

            return Ok(cultivo);
        }

        /// <summary>
        /// Deleta um cultivo pelo ID.
        /// </summary>
        /// <param name="id">ID do cultivo</param>
        /// <response code="204">Confirma a exclusão do cultivo</response>
        /// <response code="404">Se o cultivo não for encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCultivo(string id)
        {
            await _cultivoService.RemoveAsync(id);

            return NoContent();
        }
    }
}
