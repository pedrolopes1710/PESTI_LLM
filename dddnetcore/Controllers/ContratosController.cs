using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using dddnetcore.Domain.Contratos;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Authorization;

namespace DDDSample1.Controllers
{
    [Route("api/contratos")]
    [ApiController]
    public class ContratosController : ControllerBase
    {
        private readonly ContratoService _service;

        public ContratosController(ContratoService service)
        {
            _service = service;
        }

        // GET: api/contratos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoDto>>> GetAll()
        {
            var contratos = await _service.GetAllAsync();
            return Ok(contratos);
        }

        // GET: api/contratos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ContratoDto>> GetById(string id)
        {
            var contrato = await _service.GetByIdAsync(new ContratoId(id));

            if (contrato == null)
            {
                return NotFound();
            }

            return Ok(contrato);
        }

        // POST: api/contratos
        //[Authorize(Policy = "AdminRole")]
        [HttpPost]
        public async Task<ActionResult<ContratoDto>> Create([FromBody] CreatingContratoDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados de contrato inválidos.");
            }

            var contrato = await _service.AddAsync(dto);
            if (contrato == null)
            {
                return StatusCode(500, "Ocorreu um erro ao criar o contrato.");
            }

            return CreatedAtAction(nameof(GetById), new { id = contrato.Id }, contrato);
        }

        // PUT: api/contratos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ContratoDto>> Update(string id, [FromBody] EditingContratoDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("IDs não coincidem.");
            }

            try
            {
                var updatedContrato = await _service.UpdateAsync(dto);

                if (updatedContrato == null)
                {
                    return NotFound();
                }

                return Ok(updatedContrato);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/contratos/{id}
        //[Authorize(Policy = "AdminRole")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContratoDto>> HardDelete(string id)
        {
            try
            {
                var deletedContrato = await _service.DeleteAsync(new ContratoId(id));

                if (deletedContrato == null)
                {
                    return NotFound();
                }

                return Ok(deletedContrato);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
