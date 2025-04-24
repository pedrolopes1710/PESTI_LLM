using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Tarefas;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly PerfilService _service;

        public PerfilController(PerfilService service)
        {
            _service = service;
        }

        // GET: api/Perfis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Perfis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerfilDto>> GetGetById(Guid id)
        {
            var perfil = await _service.GetByIdAsync(new PerfilId(id));

            if (perfil == null)
            {
                return NotFound();
            }

            return perfil;
        }

        // POST: api/Perfis
        [HttpPost]
        public async Task<ActionResult<PerfilDto>> Create(CreatingPerfilDto dto)
        {
            var perfil = await _service.AddAsync(dto);

            return CreatedAtAction(nameof(GetGetById), new { id = perfil.Id }, perfil);
        }

        
        // PUT: api/Perfis/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PerfilDto>> Update(Guid id, PerfilDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var perfil = await _service.UpdateAsync(dto);
                
                if (perfil == null)
                {
                    return NotFound();
                }
                return Ok(perfil);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }        
        // DELETE: api/Perfis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PerfilDto>> HardDelete(Guid id)
        {
            try
            {
                var perfil = await _service.DeleteAsync(id);

                if (perfil == null)
                {
                    return NotFound();
                }

                return Ok(perfil);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }
    }
}