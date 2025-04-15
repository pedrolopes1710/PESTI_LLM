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


         public async Task<TarefaDto> DeleteAsync(Guid id)
        {
            Tarefa tarefa = await this._repo.GetByIdAsync(new TarefaId(id));

            if (tarefa == null) return null;

            // Example: Check if linked to atividade before delete (optional)
            // if ((await this._atividadeRepo.GetByTarefaIdAsync(id)).Any())
            //     throw new BusinessRuleValidationException("Cannot delete Tarefa linked to Atividade!");

            this._repo.Remove(tarefa);
            await this._unitOfWork.CommitAsync();

            return new TarefaDto(tarefa);
        }
    }
}