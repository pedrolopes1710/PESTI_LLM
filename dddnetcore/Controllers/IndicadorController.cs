using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dddnetcore.Domain.Projetos;
using dddnetcore.Services;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndicadorController : ControllerBase
    {
        private readonly IndicadorService _service;
        private readonly IProjetoRepository _projetoRepository;

        public IndicadorController(IndicadorService service, IProjetoRepository projetoRepository)
        {
            _service = service;
            _projetoRepository = projetoRepository;
        }

        [HttpPost]
        public async Task<ActionResult<IndicadorDTO>> Criar([FromBody] CriarIndicadorDTO dto)
        {
            try
            {
                var projeto = await _projetoRepository.GetByIdAsync(new ProjetoId(dto.ProjetoId));
                if (projeto == null)
                    return BadRequest("Projeto não encontrado.");

                var result = await _service.CriarAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<IndicadorDTO>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IndicadorDTO>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IndicadorDTO>> Atualizar(Guid id, [FromBody] double novoValorAtual)
        {
            try
            {
                var result = await _service.AtualizarAsync(id, novoValorAtual);
                if (result == null) return NotFound();
                return result;
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        [HttpGet("testar-hash")]
        public ActionResult<string> TestarHash()
        {
            var id = new ProjetoId(Guid.NewGuid());
            return "HashCode: " + id.GetHashCode();
        }

    }
}
