using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.AfetacaoMensais;
using Microsoft.AspNetCore.Mvc;

namespace dddnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfetacaoMensaisController : ControllerBase
    {
        private readonly AfetacaoMensalService _service;

        public AfetacaoMensaisController(AfetacaoMensalService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AfetacaoMensalDto>>> GetAll() {
            return await _service.GetAllAsync();
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<AfetacaoMensalDto>> GetById(Guid id) {
            var afetacaoMensal = await _service.GetByIdAsync(new AfetacaoMensalId(id));

            if (afetacaoMensal == null) {
                return NotFound();
            }

            return afetacaoMensal;
        }
    }
}