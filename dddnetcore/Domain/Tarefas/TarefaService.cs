using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Atividades;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Tarefas
{
    public class TarefaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITarefaRepository _repo;
        private readonly IAtividadeRepository _atividadeRepo;

        public TarefaService(IUnitOfWork unitOfWork, ITarefaRepository repo, IAtividadeRepository atividadeRepo) {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._atividadeRepo = atividadeRepo;
        }

        public async Task<List<TarefaDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(tarefa => new TarefaDto(tarefa));
        }
        
        public async Task<TarefaDto> GetByIdAsync(TarefaId id) {
            Tarefa tarefa = await this._repo.GetByIdAsync(id);

            return tarefa == null ? null : new TarefaDto(tarefa);
        }


        public async Task<TarefaDto> AddAsync(CreatingTarefaDto dto)
        {
            if (!Enum.TryParse<StatusTarefa>(dto.Status, out var status))
            {
                throw new BusinessRuleValidationException($"Status inválido: {dto.Status}");
            }

            var tarefa = new Tarefa(new NomeTarefa(dto.Nome),new DescricaoTarefa(dto.Descricao),status);

            await this._repo.AddAsync(tarefa);
            await this._unitOfWork.CommitAsync();

            return new TarefaDto(tarefa);
        }

        public async Task<TarefaDto> UpdateAsync(TarefaDto dto)
        {
            Tarefa tarefa = await this._repo.GetByIdAsync(new TarefaId(dto.Id)); 

            if (tarefa == null)
                return null;   

            // change all field
            tarefa.ChangeDescricao(new DescricaoTarefa(dto.DescricaoTarefa));
            tarefa.ChangeNome(new NomeTarefa(dto.Nome));
            tarefa.ChangeStatus(Enum.Parse<StatusTarefa>(dto.Status));
            
            await this._unitOfWork.CommitAsync();

            return new TarefaDto (tarefa);
            
        }

        /*public async Task<TarefaDto> UpdateAtividadeAsync(TarefaId tarefaId, AtividadeId atividadeId)
        {
            Tarefa tarefa = await this._repo.GetByIdAsync(tarefaId); 
            
            if (tarefa == null)
                return null;   

            // change all field
            tarefa.AddAtividade(await this._atividadeRepo.GetByIdAsync(atividadeId));
            
            await this._unitOfWork.CommitAsync();

            return new TarefaDto (tarefa);
            
        }*/


         public async Task<TarefaDto> DeleteAsync(Guid id)
        {
            Tarefa tarefa = await this._repo.GetByIdAsync(new TarefaId(id)); 

            if (tarefa == null)
                return null;   

            if (tarefa.StatusTarefa == StatusTarefa.A_Decorrer)
                throw new BusinessRuleValidationException("It is not possible to delete an active tarefa.");
            
            this._repo.Remove(tarefa);
            await this._unitOfWork.CommitAsync();

            return new TarefaDto(tarefa);
        }
    }
}