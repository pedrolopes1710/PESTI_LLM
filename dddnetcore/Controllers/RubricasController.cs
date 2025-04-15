using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace dddnetcore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RubricasController : ControllerBase
    {
        private RubricaService _service;

        public RubricasController(RubricaService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RubricaDto>>> GetAll() {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RubricaDto>> GetById(Guid id) {
            var rubrica = await _service.GetByIdAsync(new RubricaId(id));

            if (rubrica == null) {
                return NotFound();
            }

            return rubrica;
        }

        [HttpPost]
        public async Task<ActionResult<RubricaDto>> Create(CreatingRubricaDto dto) {
            try {
                RubricaDto rubrica = await _service.AddAsync(dto);
                
                return CreatedAtAction(nameof(GetById), new { id = rubrica.Id }, rubrica);
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
        public async Task<ActionResult<RubricaDto>> Delete(Guid id) {

            try {
                return await _service.DeleteAsync(id);
            } catch (NullReferenceException e) {
                return NotFound(new {e.Message});
            } catch (BusinessRuleValidationException e) {
                return BadRequest(new {e.Message});
            } catch (Exception) {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }
}