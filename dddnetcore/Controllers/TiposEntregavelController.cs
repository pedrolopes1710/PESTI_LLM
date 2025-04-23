using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.TiposEntregavel;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace dddnetcore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TiposEntregavelController : ControllerBase
    {
         private readonly TipoEntregavelService _service;

        public TiposEntregavelController(TipoEntregavelService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoEntregavelDto>>> GetAll() {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoEntregavelDto>> GetById(Guid id) {
            var tipo = await _service.GetByIdAsync(new TipoEntregavelId(id));

            if (tipo == null) {
                return NotFound();
            }

            return tipo;
        }

        [HttpPost]
        public async Task<ActionResult<TipoEntregavelDto>> Create([FromBody] CreatingTipoEntregavelDto dto) {
            if (dto == null) {
                return BadRequest("Dados de tipo de entregável inválidos.");
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoEntregavelDto>> Delete(Guid id) {
            try{
                var deleted = await _service.DeleteAsync(new TipoEntregavelId(id));
                if (deleted == null)
                    return NotFound();

                return Ok(deleted);
            }
            catch(BusinessRuleValidationException e){
                return BadRequest(new {e.Message});
            } 
            catch(Exception ex){
                return StatusCode(500, new { Message = "Erro inesperado ao eliminar o TipoEntregavel.", Details = ex.Message });
            }
            

        }
    }
}