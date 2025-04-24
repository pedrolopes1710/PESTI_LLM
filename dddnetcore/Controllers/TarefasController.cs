using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using dddnetcore.Domain.Tarefas;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly TarefaService _service;

        public TarefasController(TarefaService service)
        {
            _service = service;
        }

        // GET: api/Tarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Tarefas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaDto>> GetGetById(Guid id)
        {
            var tarefa = await _service.GetByIdAsync(new TarefaId(id));

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
        }

        // POST: api/Tarefas
        [HttpPost]
        public async Task<ActionResult<TarefaDto>> Create(CreatingTarefaDto dto)
        {
            var tarefa = await _service.AddAsync(dto);

            return CreatedAtAction(nameof(GetGetById), new { id = tarefa.Id }, tarefa);
        }

        
        // PUT: api/Tarefas/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TarefaDto>> Update(Guid id, TarefaDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var tarefa = await _service.UpdateAsync(dto);
                
                if (tarefa == null)
                {
                    return NotFound();
                }
                return Ok(tarefa);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }        
        // DELETE: api/Tarefas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TarefaDto>> HardDelete(Guid id)
        {
            try
            {
                var tarefa = await _service.DeleteAsync(id);

                if (tarefa == null)
                {
                    return NotFound();
                }

                return Ok(tarefa);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }
    }
}