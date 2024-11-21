using Microsoft.AspNetCore.Mvc;
using SolRural.Models;
using SolRural.Service;

namespace SolRural.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProprietarioController : ControllerBase
    {
        private readonly ProprietarioService _proprietarioService;

        public ProprietarioController(ProprietarioService proprietarioService)
        {
            _proprietarioService = proprietarioService;
        }

        /// <summary>
        /// Retorna todos os proprietários.
        /// </summary>
        /// <returns>Lista de proprietários</returns>
        /// <response code="200">Retorna a lista de proprietários</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Proprietario>> GetProprietarios() => await _proprietarioService.GetAsync();

        /// <summary>
        /// Retorna um proprietário pelo ID.
        /// </summary>
        /// <param name="id">ID do proprietário</param>
        /// <returns>Um proprietário</returns>
        /// <response code="200">Retorna o proprietário solicitado</response>
        /// <response code="404">Se o proprietario não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Proprietario>> GetProprietario(string id)
        {
            var proprietario = await _proprietarioService.GetAsync(id);

            if (proprietario == null)
            {
                return NotFound();
            }

            return proprietario;
        }

        /// <summary>
        /// Cria um novo proprietário.
        /// </summary>
        /// <param name="proprietario">Dados do novo proprietário</param>
        /// <returns>O proprietário criado</returns>
        /// <response code="201">Retorna o proprietário recém-criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Proprietario> PostProprietario(Proprietario proprietario)
        {
            await _proprietarioService.CreateAsync(proprietario);

            return proprietario;
        }

        /// <summary>
        /// Atualiza um proprietário existente.
        /// </summary>
        /// <param name="id">ID do proprietário</param>
        /// <param name="proprietarioAtualizado">Dados atualizados do proprietário</param>
        /// <response code="200">Retorna o proprietário atualizado</response>
        /// <response code="404">Se o proprietário não for encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProprietario(string id, Proprietario proprietarioAtualizado)
        {
            var proprietario = await _proprietarioService.GetAsync(id);

            if (proprietario == null)
            {
                return NotFound("Proprietário não encontrado.");
            }

            await _proprietarioService.UpdateAsync(id, proprietarioAtualizado);

            return Ok(proprietario);
        }

        /// <summary>
        /// Deleta um proprietário pelo ID.
        /// </summary>
        /// <param name="id">ID do proprietário</param>
        /// <response code="204">Confirma a exclusão do proprietário</response>
        /// <response code="404">Se o proprietário não for encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProprietario(string id)
        {
            await _proprietarioService.RemoveAsync(id);

            return NoContent();
        }
    }
}
