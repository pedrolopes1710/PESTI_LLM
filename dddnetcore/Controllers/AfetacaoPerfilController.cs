using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using dddnetcore.Domain.Tarefas;
using dddnetcore.Domain.Atividades;
using DDDSample1.Domain.Atividades;
using dddnetcore.Domain.Orcamentos;
using DDDSample1.Domain.AfetacaoPerfis;
using dddnetcore.Domain.AfetacaoPerfis;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfetacaoPerfilController : ControllerBase
    {
        private readonly AfetacaoPerfilService _service;

        public AfetacaoPerfilController(AfetacaoPerfilService service)
        {
            _service = service;
        }

        // GET: api/Atividades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AfetacaoPerfilDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/AfetacaoPerfil/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AfetacaoPerfilDto>> GetById(Guid id)
        {
            var afetacaoPerfil = await _service.GetByIdAsync(new AfetacaoPerfilId(id));

            if (afetacaoPerfil == null)
            {
                return NotFound();
            }

            return afetacaoPerfil;
        }

        // POST: api/AfetacaoPerfil
        [HttpPost]
        public async Task<ActionResult<AfetacaoPerfilDto>> Create(CreatingAfetacaoPerfilDto dto)
        {
            var afetacaoPerfil = await _service.AddAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = afetacaoPerfil.Id }, afetacaoPerfil);
        }

        
        // PUT: api/AfetacaoPerfil/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AfetacaoPerfilDto>> Update(Guid id, AfetacaoPerfilDto dto)
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
        // DELETE: api/AfetacaoPerfil/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AfetacaoPerfilDto>> HardDelete(Guid id)
        {
            try
            {
                var afetacaoPerfil = await _service.DeleteAsync(new AfetacaoPerfilId(id));

                if (afetacaoPerfil == null)
                {
                    return NotFound();
                }

                return Ok(afetacaoPerfil);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }
    }
}