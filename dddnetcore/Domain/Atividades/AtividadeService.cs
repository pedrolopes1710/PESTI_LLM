using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using dddnetcore.Domain.Tarefas;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Projetos;
using dddnetcore.Domain.Entregaveis;
using dddnetcore.Domain.Perfis;

namespace DDDSample1.Domain.Atividades
{
    public class AtividadeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAtividadeRepository _repo;
        private readonly ITarefaRepository _repoTarefa;
        private readonly IOrcamentoRepository _repoOrcamento;
        private readonly IEntregavelRepository _repoEntregavel;
        private readonly IPerfilRepository _repoPerfil;

    


        public AtividadeService(IUnitOfWork unitOfWork, IAtividadeRepository repo, ITarefaRepository repoTarefa, IOrcamentoRepository repoOrcamento, IEntregavelRepository repoEntregavel, IPerfilRepository repoPerfil) 
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoTarefa = repoTarefa;
            this._repoOrcamento = repoOrcamento;
            this._repoEntregavel = repoEntregavel;
            this._repoPerfil = repoPerfil;
        }

        public async Task<List<AtividadeDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(atividade => new AtividadeDto(atividade));
        }

        public async Task<AtividadeDto> GetByIdAsync(AtividadeId id) {
            Atividade atividade = await this._repo.GetByIdAsync(id);

            return atividade == null ? null : new AtividadeDto(atividade);
        }

        public async Task<AtividadeDto> AddAsync(CreatingAtividadeDto dto)
        {
            Orcamento? orcamento = null;
            if (dto.OrcamentoId.HasValue)
            {
                orcamento = await _repoOrcamento.GetByIdAsync(new OrcamentoId(dto.OrcamentoId.Value));
            }            


        
            var atividade = new Atividade(new DataFimAtividade(dto.DataFimAtividade), new DataInicioAtividade(dto.DataInicioAtividade), new DescricaoAtividade(dto.DescricaoAtividade), new NomeAtividade(dto.NomeAtividade),orcamento);


            await this._repo.AddAsync(atividade);
            await this._unitOfWork.CommitAsync();

             // associar tarefas
            if (dto.TarefasIds != null)
            {
                foreach (var tarefaId in dto.TarefasIds)
                {
                    var tarefa = await _repoTarefa.GetByIdAsync(new TarefaId(tarefaId));
                    if (tarefa != null)
                    {
                        tarefa.SetAtividadeId(atividade.Id); // método novo a criar na entidade
                        await _repoTarefa.UpdateAsync(tarefa); // atualizar a tarefa com a nova atividade
                    }
                }
                await _unitOfWork.CommitAsync(); // segundo commit para guardar associações
            }
            if (dto.EntregaveisIds != null)
            {
                foreach (var entregavelId in dto.EntregaveisIds)
                {
                    var entregavel = await _repoEntregavel.GetByIdAsync(new EntregavelId(entregavelId));
                    if (entregavel != null)
                    {
                        entregavel.SetAtividadeId(atividade.Id); // método novo a criar na entidade
                        await _repoEntregavel.UpdateAsync(entregavel); // atualizar o entregável com a nova atividade
                    }
                }
                await _unitOfWork.CommitAsync(); // segundo commit para guardar associações
            }
            if (dto.PerfisIds != null)
            {
                foreach (var perfilId in dto.PerfisIds)
                {
                    var perfil = await _repoPerfil.GetByIdAsync(new PerfilId(perfilId));
                    if (perfil != null)
                    {
                        perfil.SetAtividadeId(atividade.Id); // método novo a criar na entidade
                        await _repoPerfil.UpdateAsync(perfil); // atualizar o perfil com a nova atividade
                    }
                }
                await _unitOfWork.CommitAsync(); // segundo commit para guardar associações
            }


            return new AtividadeDto(atividade);
        }

        public async Task<AtividadeDto> UpdateAsync(AtividadeDto dto)
        {
            var atividade = await this._repo.GetByIdAsync(new AtividadeId(dto.Id)); 

            if (atividade == null)
                return null;   

            // change all fields
            atividade.ChangeDescricaoAtividade(new DescricaoAtividade(dto.DescricaoAtividade));
            atividade.ChangeNomeAtividade(new NomeAtividade(dto.NomeAtividade));
            atividade.ChangeDataInicioAtividade(new DataInicioAtividade(dto.DataInicioAtividade));
            atividade.ChangeDataFimAtividade(new DataFimAtividade(dto.DataFimAtividade));
            
            
            await this._unitOfWork.CommitAsync();

            return new AtividadeDto(atividade);
        }
               
        public async Task<AtividadeDto> DeleteAsync(AtividadeId id)
        {
            var atividade = await this._repo.GetByIdAsync(id); 

            if (atividade == null)
                return null;   

            this._repo.Remove(atividade);
            await this._unitOfWork.CommitAsync();

            return new AtividadeDto(atividade);
        }

        private async Task checkTarefaIdAsync(TarefaId tarefaId)
        {
           var tarefa = await _repoTarefa.GetByIdAsync(tarefaId);
           if (tarefa == null)
                throw new BusinessRuleValidationException("Invalid Tarefa Id.");
        }
    }
}