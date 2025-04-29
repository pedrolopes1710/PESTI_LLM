using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Orcamentos
{
    public class OrcamentoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrcamentoRepository _repo;
        private readonly IRubricaRepository _rubricaRepo;

        public OrcamentoService(IUnitOfWork unitOfWork, IOrcamentoRepository repo, IRubricaRepository rubricaRepo) {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._rubricaRepo = rubricaRepo;
        }

        public async Task<List<OrcamentoDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(orcamento => new OrcamentoDto(orcamento));
        }

        public async Task<OrcamentoDto> GetByIdAsync(OrcamentoId id) {
            Orcamento orcamento = await this._repo.GetByIdAsync(id);

            return orcamento == null ? null : new OrcamentoDto(orcamento);
        }

        /*public async Task<OrcamentoDto> AddAsync(CreatingOrcamentoDto dto) {
            Orcamento orcamento = new
        }*/

        public async Task<OrcamentoDto> EditOrcamentoAsync(OrcamentoId id, EditingOrcamentoDto dto) {
            Orcamento orcamento = await this._repo.GetByIdAsync(id) ?? throw new NullReferenceException("Not Found Budget: " + id);
        
            //TODO quando tiver a parte das afeçtações completas,
            /*
            *Meter para editar rubricas, pois se se mudar para rubrica salarial,
            *tem de se calcular automaticamente os gastos.
            */
            

            if (dto.GastoPlaneado != null) {
                if (orcamento.Rubrica.Nome.Nome.Equals(NomeRubrica.NomeSalarial)) 
                    throw new BusinessRuleValidationException("Cannot change value of salarial budget.");
                orcamento.MudarGastoPlaneado(new GastoPlaneado(dto.GastoPlaneado.Value));
            }


            await this._repo.UpdateAsync(orcamento);
            await this._unitOfWork.CommitAsync();

            return new OrcamentoDto(orcamento);
        }

    }
}