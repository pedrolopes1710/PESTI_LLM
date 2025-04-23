using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Entregaveis;
using dddnetcore.Domain.Entregavel;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace dddnetcore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EntregaveisController : ControllerBase
    {
         private readonly EntregavelService _service;

        public EntregaveisController(EntregavelService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntregavelDto>>> GetAll() {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EntregavelDto>> GetById(Guid id) {
            var tipo = await _service.GetByIdAsync(new EntregavelId(id));

            if (tipo == null) {
                return NotFound();
            }

            return tipo;
        }

        [HttpPost]
        public async Task<ActionResult<EntregavelDto>> Create([FromBody] CreatingEntregavelDto dto) {
            if (dto == null) {
                return BadRequest("Dados de tipo de entregável inválidos.");
            }
            try{
                var entregavel = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = entregavel.Id }, entregavel);
            }catch(BusinessRuleValidationException e){
                return BadRequest(new {e.Message});
            }catch(Exception ex){
                return StatusCode(500, new { Message = "Erro inesperado ao criar o Entregável.", Details = ex.Message });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EntregavelDto>> Update(Guid id, EntregavelDto dto){
            if (id != dto.Id) {
                return BadRequest("ID do entregável não coincide com o ID fornecido.");
            }
            try
            {
                var updatedEntregavel = await _service.UpdateAsync(dto);

                if (updatedEntregavel == null)
                    return NotFound();

                return Ok(updatedEntregavel);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro inesperado", Details = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<EntregavelDto>> Delete(Guid id) {
            try{
                var entregavel = await _service.DeleteAsync(new EntregavelId(id));
                if (entregavel == null)
                    return NotFound();

                return Ok(entregavel);
            }
            catch(BusinessRuleValidationException e){
                return BadRequest(new {e.Message});
            } 
            catch(Exception ex){
                return StatusCode(500, new { Message = "Erro inesperado ao eliminar o Entregável.", Details = ex.Message });
            }
            

        }
    }
}