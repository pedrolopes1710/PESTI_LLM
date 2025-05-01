using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Projetos;
using dddnetcore.Domain.Tarefas;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace dddnetcore.Domain.AfetacaoPerfis
{
    public class AfetacaoPerfilDto
    {
        public Guid Id {get;set;}
        public int DuracaoMes {get;set;}
        public double PMsAprovados {get;set;}
        public PerfilDto PerfilDto {get;set;}

        public AfetacaoPerfilDto() {}

        public AfetacaoPerfilDto(AfetacaoPerfil afetacaoPerfil) {
            this.Id = afetacaoPerfil.Id.AsGuid();
            this.PerfilDto = afetacaoPerfil.Perfil != null ? new PerfilDto(afetacaoPerfil.Perfil) : null;
            this.DuracaoMes = afetacaoPerfil.DuracaoMes.Quantidade;
            this.PMsAprovados = afetacaoPerfil.PMsAprovados.Quantidade;
        }
    }
}