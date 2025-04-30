using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Orcamentos;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace dddnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrcamentosController    : ControllerBase
    {
        private readonly OrcamentoService _service;

        public OrcamentosController(OrcamentoService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrcamentoDto>>> GetAll() {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrcamentoDto>> GetById(Guid id) {
            var orcamento = await _service.GetByIdAsync(new OrcamentoId(id));

            if (orcamento == null) {
                return NotFound();
            }

            return orcamento;
        }

        [HttpPost]
        public async Task<ActionResult<OrcamentoDto>> Create(CreatingOrcamentoDto dto) {
            try {
                OrcamentoDto orcamento = await _service.AddAsync(dto);
                
                return CreatedAtAction(nameof(GetById), new { id = orcamento.Id }, orcamento);
            } catch (BusinessRuleValidationException e) {
                return BadRequest(new {e.Message});
            } catch (NullReferenceException e) {
                return NotFound(new {e.Message});
            } catch (ArgumentNullException e) {
                return BadRequest(new {e.Message});
            } catch (Exception) {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<OrcamentoDto>> Edit(Guid id, EditingOrcamentoDto dto) {
            try {
                var orcamentoDto = await _service.EditOrcamentoAsync(new OrcamentoId(id), dto);
                return Ok(orcamentoDto);
            } catch (NullReferenceException e) {
                return NotFound(new {e.Message});
            } catch (ArgumentNullException e) {
                return BadRequest(new {e.Message});
            } catch (Exception) {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }
}