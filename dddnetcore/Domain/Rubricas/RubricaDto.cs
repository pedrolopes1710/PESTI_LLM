using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Rubricas
{
    public class RubricaDto
    {
        public Guid Id {get;set;}
        public string Nome {get;set;}

        public RubricaDto() {}

        public RubricaDto(Rubrica rubrica)
        {
            this.Id = rubrica.Id.AsGuid();
            this.Nome = rubrica.Nome.Nome;
        }   
    }
}