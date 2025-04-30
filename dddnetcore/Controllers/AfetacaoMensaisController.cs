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
    public class AfetacaoMensaisController
    {
        private readonly AfetacaoMensalService _service;

        public AfetacaoMensaisController(AfetacaoMensalService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AfetacaoMensalDto>>> GetAll() {
            return await _service.GetAllAsync();
        } 
    }
}