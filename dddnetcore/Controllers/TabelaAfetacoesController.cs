using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using dddnetcore.Domain.TabelaAfetacoes;
using Microsoft.AspNetCore.Mvc;

namespace dddnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabelaAfetacoesController : ControllerBase
    {
        private readonly TabelaAfetacoesService _service;

        public TabelaAfetacoesController(TabelaAfetacoesService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TabelaAfetacoesDto>> GetById(Guid id)
        {
            var tabelaAfetacoes = await _service.GenerateAsync(id);

            if (tabelaAfetacoes == null)
            {
                return NotFound();
            }

            return tabelaAfetacoes;
        }
    }
}