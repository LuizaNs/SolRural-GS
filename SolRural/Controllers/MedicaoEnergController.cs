using Microsoft.AspNetCore.Mvc;
using SolRural.Models;
using SolRural.Service;

namespace SolRural.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicaoEnergController : ControllerBase
    {
        private readonly MedicaoEnergService _medicaoEnergService;

        public MedicaoEnergController(MedicaoEnergService medicaoEnergService)
        {
            _medicaoEnergService = medicaoEnergService;
        }

        /// <summary>
        /// Retorna todas as medições de energia.
        /// </summary>
        /// <returns>Lista de medições de energia</returns>
        /// <response code="200">Retorna a lista de medições de energia</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<MedicaoEnerg>> GetMedicoesEnerg() => await _medicaoEnergService.GetAsync();

        /// <summary>
        /// Retorna uma medição de energia pelo ID.
        /// </summary>
        /// <param name="id">ID da medição de energia</param>
        /// <returns>Uma medição de energia</returns>
        /// <response code="200">Retorna a medição de energia solicitada</response>
        /// <response code="404">Se a medição de energia não for encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicaoEnerg>> GetMedicaoEnerg(string id)
        {
            var medicaoEnerg = await _medicaoEnergService.GetAsync(id);

            if (medicaoEnerg == null)
            {
                return NotFound();
            }

            return medicaoEnerg;
        }

        /// <summary>
        /// Cria uma nova mediçaõ de energia.
        /// </summary>
        /// <param name="medicaoEnerg">Dados da nova medição de energia</param>
        /// <returns>A medição de energia criada</returns>
        /// <response code="201">Retorna a medição de energia recém-criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<MedicaoEnerg> PostMedicaoEnerg(MedicaoEnerg medicaoEnerg)
        {
            await _medicaoEnergService.CreateAsync(medicaoEnerg);

            return medicaoEnerg;
        }

        /// <summary>
        /// Atualiza uma medição de energia existente.
        /// </summary>
        /// <param name="id">ID da medição de energia</param>
        /// <param name="medicaoEnergAtualizada">Dados atualizados da medição de energia</param>
        /// <response code="200">Retorna a medição de energia atualizada</response>
        /// <response code="404">Se a medição de energia não for encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMedicaoEnerg(string id, MedicaoEnerg medicaoEnergAtualizada)
        {
            var medicaoEnerg = await _medicaoEnergService.GetAsync(id);

            if (medicaoEnerg == null)
            {
                return NotFound("Medição de energia não encontrada.");
            }

            await _medicaoEnergService.UpdateAsync(id, medicaoEnergAtualizada);

            return Ok(medicaoEnerg);
        }

        /// <summary>
        /// Deleta uma medição de energia pelo ID.
        /// </summary>
        /// <param name="id">ID da medição de energia</param>
        /// <response code="204">Confirma a exclusão da medição de energia</response>
        /// <response code="404">Se a medição de energia não for encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMedicaoEnerg(string id)
        {
            await _medicaoEnergService.RemoveAsync(id);

            return NoContent();
        }
    }
}
