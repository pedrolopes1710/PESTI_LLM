using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Orcamentos;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Rubricas
{
    public class RubricaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRubricaRepository _repo;
        private readonly IOrcamentoRepository _orcamentoRepo;

        public RubricaService(IUnitOfWork unitOfWork, IRubricaRepository repo, IOrcamentoRepository orcamentoRepo) {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._orcamentoRepo = orcamentoRepo;
        }

        public async Task<List<RubricaDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(rubrica => new RubricaDto(rubrica));
        }
        
        public async Task<RubricaDto> GetByIdAsync(RubricaId id) {
            Rubrica rubrica = await this._repo.GetByIdAsync(id);

            return rubrica == null ? null : new RubricaDto(rubrica);
        }

        public async Task<RubricaDto> AddAsync(CreatingRubricaDto dto) {
            Rubrica rubrica = new Rubrica(dto.Nome);

            await this._repo.AddAsync(rubrica);
            await this._unitOfWork.CommitAsync();

            return new RubricaDto(rubrica);
        }

        public async Task<RubricaDto> DeleteAsync(Guid id) {
            Rubrica rubrica = await this._repo.GetByIdAsync(new RubricaId(id));

            if (rubrica == null) return null;

            if ((await this._orcamentoRepo.GetOrcamentosAsync(rubricaId: id)).Count != 0)
                throw new BusinessRuleValidationException("Cannot remove used RUBRICA!"); //TODO traduzir rubrica para inglês

            this._repo.Remove(rubrica);
            await this._unitOfWork.CommitAsync();

            return new RubricaDto(rubrica);
        }
    }
}