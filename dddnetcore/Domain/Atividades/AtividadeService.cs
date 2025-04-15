using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Tarefas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Atividades
{
    public class AtividadeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAtividadeRepository _repo;
        private readonly ITarefaRepository _tarefaRepo;

        public AtividadeService(IUnitOfWork unitOfWork, IAtividadeRepository repo, ITarefaRepository tarefaRepo) {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._tarefaRepo = tarefaRepo;
        }

        public async Task<List<AtividadeDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(atividade => new AtividadeDto(atividade));
        }

        public async Task<AtividadeDto> GetByIdAsync(AtividadeId id) {
            Atividade atividade = await this._repo.GetByIdAsync(id);

            return atividade == null ? null : new AtividadeDto(atividade);
        }

        /*public async Task<OrcamentoDto> AddAsync(CreatingOrcamentoDto dto) {
            Orcamento orcamento = new
        }*/

        /*public async Task<AtividadeDto> EditAtividadeAsync(AtividadeId id, EditingAtividadeDto dto) {
            Atividade atividade = await this._repo.GetByIdAsync(id) ?? throw new NullReferenceException("Not Found Budget: " + id);
        
           
            if (dto.GastoPlaneado != null) {
                orcamento.MudarGastoExecutado(new GastoPlaneado(dto.GastoPlaneado.Value));
            }
            if (dto.GastoExecutado != null) {
                orcamento.MudarGastoExecutado(new GastoExecutado(dto.GastoExecutado.Value));
            }


            await this._repo.UpdateAsync(orcamento);
            await this._unitOfWork.CommitAsync();

            return new OrcamentoDto(orcamento);
        }*/

    }
}