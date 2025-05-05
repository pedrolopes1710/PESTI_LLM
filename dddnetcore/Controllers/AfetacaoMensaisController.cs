using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.AfetacaoMensais;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace dddnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfetacaoMensaisController : ControllerBase
    {
        private readonly AfetacaoMensalService _service;

        public AfetacaoMensaisController(AfetacaoMensalService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AfetacaoMensalDto>>> GetAll() {
            return await _service.GetAllAsync();
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<AfetacaoMensalDto>> GetById(Guid id) {
            var afetacaoMensal = await _service.GetByIdAsync(new AfetacaoMensalId(id));

            if (afetacaoMensal == null) {
                return NotFound();
            }

            return afetacaoMensal;
        }

        [HttpPost]
        public async Task<ActionResult<AfetacaoMensalDto>> Create(CreatingAfetacaoMensalDto dto) {
            try {
                var afetacaoMensal = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = afetacaoMensal.Id }, afetacaoMensal);
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
        public async Task<ActionResult<AfetacaoMensalDto>> Edit(Guid id, EditingAfetacaoMensalDto dto) {
            try {
                var afetacaoMensalDto = await _service.UpdateAsync(new AfetacaoMensalId(id), dto);
                return Ok(afetacaoMensalDto);
            } catch (NullReferenceException e) {
                return NotFound(new {e.Message});
            } catch (ArgumentNullException e) {
                return BadRequest(new {e.Message});
            } catch (Exception) {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            try {
                await _service.RemoveAsync(new AfetacaoMensalId(id));
                return NoContent();
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