using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.TiposVinculo;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace dddnetcore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TiposVinculoController : ControllerBase
    {
         private readonly TipoVinculoService _service;

        public TiposVinculoController(TipoVinculoService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoVinculoDto>>> GetAll() {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoVinculoDto>> GetById(Guid id) {
            var tipo = await _service.GetByIdAsync(new TipoVinculoId(id));

            if (tipo == null) {
                return NotFound();
            }

            return tipo;
        }

        [HttpPost]
        public async Task<ActionResult<TipoVinculoDto>> Create([FromBody] CreatingTipoVinculoDto dto) {
            if (dto == null) {
                return BadRequest("Dados de tipo de vínculo inválidos.");
            }
            try{
                var created = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }catch(BusinessRuleValidationException e){
                return BadRequest(new {e.Message});
            }catch(Exception ex){
                return StatusCode(500, new { Message = "Erro inesperado ao criar o TipoEntregavel.", Details = ex.Message });
            }
            
        }


        /*[HttpPut("{id}")]
        public async Task<ActionResult<TipoVinculoDto>> Update(Guid id, TipoVinculoDto dto){
            if (id != dto.Id) {
                return BadRequest("ID do tipo de vínculo não coincide com o ID fornecido.");
            }
            try
            {
                var updatedTipo = await _service.UpdateAsync(dto);

                if (updatedTipo == null)
                    return NotFound();

                return Ok(updatedTipo);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro inesperado", Details = ex.Message });
            }
        }*/

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoVinculoDto>> Delete(Guid id) {
            try{
                var deleted = await _service.DeleteAsync(new TipoVinculoId(id));
                if (deleted == null)
                    return NotFound();

                return Ok(deleted);
            }
            catch(BusinessRuleValidationException e){
                return BadRequest(new {e.Message});
            } 
            catch(Exception ex){
                return StatusCode(500, new { Message = "Erro inesperado ao eliminar o TipoVinculo.", Details = ex.Message });
            }
            

        }
    }
}