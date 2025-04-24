using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Projetos;
using dddnetcore.Domain.Tarefas;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace dddnetcore.Domain.Atividades
{
    public class AtividadeDto
    {
        public Guid Id {get;set;}
        public DateTime DataFimAtividade {get;set;}
        public DateTime DataInicioAtividade {get;set;}
        public String DescricaoAtividade {get;set;}
        public String NomeAtividade {get;set;}
        public TarefaDto TarefaDto {get;set;}
        public OrcamentoDto OrcamentoDto {get;set;}

        

        public AtividadeDto() {}

        public AtividadeDto(Atividade atividade) {
            this.Id = atividade.Id.AsGuid();
            this.DataFimAtividade = atividade.DataFimAtividade.FimAtividade;
            this.DataInicioAtividade = atividade.DataInicioAtividade.InicioAtividade;
            this.DescricaoAtividade = atividade.DescricaoAtividade.Descricao;
            this.NomeAtividade = atividade.NomeAtividade.Nome;
            this.TarefaDto = atividade.Tarefa != null ? new TarefaDto(atividade.Tarefa) : null;
            this.OrcamentoDto = atividade.Orcamento != null ? new OrcamentoDto(atividade.Orcamento) : null;
        }
    }
}