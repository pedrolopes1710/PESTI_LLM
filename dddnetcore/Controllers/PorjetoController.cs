using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> GetAll()
        {
            var projetos = await _service.GetAllAsync();
            return Ok(projetos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjetoDto dto)
        {
            var projeto = await _service.CreateAsync(dto.Nome, dto.Descricao);
            return CreatedAtAction(nameof(GetAll), new { id = projeto.Id }, projeto);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }

    public class CreateProjetoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}