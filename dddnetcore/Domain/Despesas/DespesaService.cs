using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.CargasMensais;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Despesas
{
    public class DespesaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDespesaRepository _repo;
        private readonly ICargaMensalRepository _cargaMensalRepo;

        public DespesaService(IUnitOfWork unitOfWork, IDespesaRepository repo, ICargaMensalRepository cargaMensalRepository) {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._cargaMensalRepo = cargaMensalRepository;
        }

        public async Task<List<DespesaDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(despesa => new DespesaDto(despesa));
        }

        public async Task<DespesaDto> GetByIdAsync(DespesaId id) {
            Despesa despesa = await this._repo.GetByIdAsync(id);

            return despesa == null ? null : new DespesaDto(despesa);
        }

        public async Task<DespesaDto> AddAsync(CreatingDespesaDto dto) {
            Despesa despesa;
            if (dto.CargaMensalId == null) {
                    if (dto.Descricao == null) 
                throw new ArgumentNullException("Missing name for Expense creation!");

                despesa = new Despesa(new DescricaoDespesa(dto.Descricao), new ValorDespesa(dto.Valor), null);
            } else {
                CargaMensal cargaMensal = await this._cargaMensalRepo.GetByIdAsync(new CargaMensalId(dto.CargaMensalId!.Value)) ?? throw new NullReferenceException("Monthly Load not found!");

                //TODO: qd a pessoa existir, criar despesa com a carga mensal
                throw new NotImplementedException("Not implemented yet!");
            }
            
            await this._repo.AddAsync(despesa);
            await this._unitOfWork.CommitAsync();

            return new DespesaDto(despesa);
        }
    }
}