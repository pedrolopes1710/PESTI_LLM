using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dddnetcore.Services;

namespace dddnetcore.Controllers
{
    [ApiController]
    [Route("api/projetos")]
    public class ProjetoController : ControllerBase
    {
        private readonly ProjetoService _service;

        public ProjetoController(ProjetoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjetoDTO>>> GetAll()
        {
            var projetos = await _service.GetAllAsync();
            return Ok(projetos);
        }

        [HttpPost]
        public async Task<ActionResult<ProjetoDTO>> Create([FromBody] CreateProjetoDto dto)
        {
            var projeto = await _service.CreateAsync(dto.Nome, dto.Descricao);
            return CreatedAtAction(nameof(GetAll), new { id = projeto.Id }, projeto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjetoDTO>> GetById(Guid id)
        {
            var projeto = await _service.GetByIdAsync(id);
            if (projeto == null) return NotFound();
            return Ok(projeto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjetoDTO>> Update(Guid id, [FromBody] UpdateProjetoDto dto)
        {
            var projeto = await _service.UpdateAsync(id, dto.Nome, dto.Descricao);
            if (projeto == null) return NotFound();
            return Ok(projeto);
        }

    }

}