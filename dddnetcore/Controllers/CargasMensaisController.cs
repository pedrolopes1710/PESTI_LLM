using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using dddnetcore.Domain.Contratos;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using dddnetcore.Domain.CargasMensais;

namespace DDDSample1.Controllers
{
    [Route("api/cargasMensais")]
    [ApiController]
    public class CargasMensaisController : ControllerBase
    {
        private readonly CargaMensalService _service;

        public CargasMensaisController(CargaMensalService service)
        {
            _service = service;
        }

        // GET: api/cargasMensais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargaMensalDto>>> GetAll()
        {
            var cargasMensais = await _service.GetAllAsync();
            return Ok(cargasMensais);
        }

        // GET: api/cargasMensais/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CargaMensalDto>> GetById(string id)
        {
            var cargaMensal = await _service.GetByIdAsync(new CargaMensalId(id));

            if (cargaMensal == null)
            {
                return NotFound();
            }

            return Ok(cargaMensal);
        }

        // POST: api/cargasMensais
        //[Authorize(Policy = "AdminRole")]
        [HttpPost]
        public async Task<ActionResult<CargaMensalDto>> Create([FromBody] CreatingCargaMensalDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados de carga mensal inválidos.");
            }

            var cargaMensal = await _service.AddAsync(dto);
            if (cargaMensal == null)
            {
                return StatusCode(500, "Ocorreu um erro ao criar a carga mensal.");
            }

            return CreatedAtAction(nameof(GetById), new { id = cargaMensal.Id }, cargaMensal);
        }

        [HttpPost("bulk")]
        public async Task<ActionResult<IEnumerable<CargaMensalDto>>> CreateBulk([FromBody] CreatingBulkCargaMensalDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados de carga mensal inválidos.");
            }

            try
            {
                var cargasMensais = await _service.AddBulkAsync(dto);
                return CreatedAtAction(nameof(GetAll), cargasMensais);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro ao criar as cargas mensais.", Details = ex.Message });
            }

        }

        // PUT: api/cargasMensais/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<CargaMensalDto>> Update(Guid id, [FromBody] EditingCargaMensalDto dto)
        {
            try
            {
                var updatedCargaMensal = await _service.UpdateAsync(new CargaMensalId(id), dto);

                if (updatedCargaMensal == null)
                {
                    return NotFound();
                }

                return Ok(updatedCargaMensal);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/cargasMensais/{id}
        //[Authorize(Policy = "AdminRole")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CargaMensalDto>> HardDelete(string id)
        {
            try
            {
                var deletedCargaMensal = await _service.DeleteAsync(new CargaMensalId(id));

                if (deletedCargaMensal == null)
                {
                    return NotFound();
                }

                return Ok(deletedCargaMensal);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
