using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Despesas;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace dddnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly DespesaService _service;

        public DespesasController(DespesaService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DespesaDto>>> GetAll() {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DespesaDto>> GetById(Guid id) {
            var despesa = await _service.GetByIdAsync(new DespesaId(id));

            if (despesa == null) {
                return NotFound();
            }

            return despesa;
        }

        [HttpPost]
        public async Task<ActionResult<DespesaDto>> Create(CreatingDespesaDto dto) {
            try {
                DespesaDto despesa = await _service.AddAsync(dto);
                 
                return CreatedAtAction(nameof(GetById), new { id = despesa.Id }, despesa);
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

        [HttpPost("generate")]
        public async Task<ActionResult<List<DespesaDto>>> Generate(GeneratingDespesaDto dto) {
            try {
                List<DespesaDto> despesas = await _service.GenerateAsync(dto);
                return CreatedAtAction(nameof(GetAll), despesas);
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
        public async Task<ActionResult<DespesaDto>> Update(Guid id, EditingDespesaDto dto) {
            try {
                DespesaDto despesa = await _service.EditAsync(new DespesaId(id), dto);
                return Ok(despesa);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<DespesaDto>> Delete(Guid id) {
            try {
                return await _service.DeleteAsync(new DespesaId(id));
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
    }
}