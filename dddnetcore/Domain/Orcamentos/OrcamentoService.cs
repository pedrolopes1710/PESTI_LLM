using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Despesas;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Orcamentos
{
    public class OrcamentoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrcamentoRepository _repo;
        private readonly IRubricaRepository _rubricaRepo;
        private readonly IDespesaRepository _despesaRepo;

        public OrcamentoService(IUnitOfWork unitOfWork, IOrcamentoRepository repo, IRubricaRepository rubricaRepo, IDespesaRepository despesaRepo) {    
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._rubricaRepo = rubricaRepo;
            this._despesaRepo = despesaRepo;
        }

        public async Task<List<OrcamentoDto>> GetAllAsync(Guid? atividadeId = null) {
            if (atividadeId != null) {
                return (await this._repo.GetOrcamentosByAtividadeAsync(atividadeId)).ConvertAll(orcamento => new OrcamentoDto(orcamento));
            }
            return (await this._repo.GetAllAsync()).ConvertAll(orcamento => new OrcamentoDto(orcamento));
        }

        public async Task<OrcamentoDto> GetByIdAsync(OrcamentoId id) {
            Orcamento orcamento = await this._repo.GetByIdAsync(id);

            return orcamento == null ? null : new OrcamentoDto(orcamento);
        }

        public async Task<OrcamentoDto> AddAsync(CreatingOrcamentoDto dto) {
            //hj descobri q double e Guid nunca sao null

            Rubrica rubrica = await this._rubricaRepo.GetByIdAsync(new RubricaId(dto.RubricaId)) ?? throw new NullReferenceException("Not Found Rubric: " + dto.RubricaId);
            Orcamento orcamento = new Orcamento(new GastoPlaneado(dto.GastoPlaneado), rubrica);

            await this._repo.AddAsync(orcamento);
            await this._unitOfWork.CommitAsync();

            return new OrcamentoDto(orcamento);
        }

        public async Task<OrcamentoDto> EditOrcamentoAsync(OrcamentoId id, EditingOrcamentoDto dto) {
            Orcamento orcamento = await this._repo.GetByIdAsync(id) ?? throw new NullReferenceException("Not Found Budget: " + id);
        
            if (dto.GastoPlaneado != null) {
                orcamento.MudarGastoPlaneado(new GastoPlaneado(dto.GastoPlaneado.Value));
            }

            if (dto.RubricaId != null) {
                Rubrica rubrica = await this._rubricaRepo.GetByIdAsync(new RubricaId(dto.RubricaId.Value)) ?? throw new NullReferenceException("Not Found Rubric: " + id);
                orcamento.MudarRubrica(rubrica);
            }   

            if (dto.DespesaId != null) {
                Despesa despesa = await this._despesaRepo.GetByIdAsync(new DespesaId(dto.DespesaId.Value)) ?? throw new NullReferenceException("Not Found Expense: " + id);
                orcamento.AddDespesa(despesa);
            }


            await this._repo.UpdateAsync(orcamento);
            await this._unitOfWork.CommitAsync();

            return new OrcamentoDto(orcamento);
        }

    }
}