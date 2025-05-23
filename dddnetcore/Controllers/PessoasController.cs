using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Controllers
{
    [Route("api/pessoas")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaService _service;

        public PessoaController(PessoaService service)
        {
            _service = service;
        }


        // GET: api/pessoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaDto>>> GetAll()
        {
            var pessoas = await _service.GetAllAsync();
            return Ok(pessoas);
        }

        // GET: api/pessoas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaDto>> GetById(string id)
        {
            try
            {
                var pessoa = await _service.GetByIdAsync(new PessoaId(Guid.Parse(id)));
                return Ok(pessoa);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(new { ex.Message });
            }
        }


        // POST: api/pessoas
        //[Authorize(Policy = "AdminRole")]
        [HttpPost]
        public async Task<ActionResult<PessoaDto>> Create([FromBody] CreatingPessoaDto dto)
        {
            try
            {
                var pessoa = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }


        // PUT: api/pessoas/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<PessoaDto>> Update(string id, [FromBody] EditingPessoaDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _service.UpdateAsync(dto);
                return Ok(updated);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(new { ex.Message });
            }
        }


        // DELETE: api/pessoas/{id}
        //[Authorize(Policy = "AdminRole")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PessoaDto>> Delete(string id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(new PessoaId(Guid.Parse(id)));
                return Ok(deleted);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(new { ex.Message });
            }
        }


        // SOFT DELETE: api/pessoas/{id}/desativar
        [HttpDelete("{id}/desativar")]
        public async Task<ActionResult<PessoaDto>> DesativarPessoa(string id)
        {
            try
            {
                var pessoa = await _service.DesativarPessoaAsync(id);
                return Ok(pessoa);
            }
            catch (BusinessRuleValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro inesperado: {e.Message}");
            }
        }

        // REATIVAR: api/pessoas/{id}/reativar
        [HttpPut("{id}/reativar")]
        public async Task<ActionResult<PessoaDto>> ReativarPessoa(string id)
        {
            try
            {
                var pessoa = await _service.ReativarPessoaAsync(id);
                return Ok(pessoa);
            }
            catch (BusinessRuleValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro inesperado: {e.Message}");
            }
        }
        



        // POST PROJETOS: api/pessoas/{id}/associarProjetos
        [HttpPost("{id}/associarProjetos")]
        public async Task<ActionResult<PessoaDto>> AssociarProjetos(string id, [FromBody] List<string> projetosIds)
        {
            var dto = new AssociarProjetoDto
            {
                PessoaId = id,
                ProjetosIds = projetosIds
            };

            try
            {
                var result = await _service.AssociarProjetosAsync(dto);
                return Ok(result);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }


        // DELETE: api/pessoas/{id}/desassociarProjetos
        [HttpPost("{id}/desassociarProjetos")]
        public async Task<ActionResult<PessoaDto>> DesassociarProjetos(string id, [FromBody] List<string> projetosIds)
        {
            var dto = new AssociarProjetoDto
            {
                PessoaId = id,
                ProjetosIds = projetosIds
            };

            try
            {
                var result = await _service.DesassociarProjetosAsync(dto);
                return Ok(result);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }



        [HttpPut("{id}/desassociarContrato")]
        public async Task<ActionResult<PessoaDto>> RemoverContrato(string id)
        {
            try
            {
                var result = await _service.RemoverContratoAsync(new PessoaId(Guid.Parse(id)));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

[HttpPut("{id}/associarContrato/{contratoId}")]
public async Task<ActionResult<PessoaDto>> AssociarContrato(string id, string contratoId)
{
    try
    {
        var result = await _service.AssociarContratoAsync(id, contratoId);
        return Ok(result);
    }
    catch (NullReferenceException ex)
    {
        return NotFound(new { ex.Message });
    }
    catch (InvalidOperationException ex)
    {
        return Conflict(new { ex.Message });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { ex.Message });
    }
}



    }
}
