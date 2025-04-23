using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using dddnetcore.Domain.TiposEntregavel;
using dddnetcore.Domain.Entregavel;

namespace dddnetcore.Domain.Entregaveis
{
    public class EntregavelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoEntregavelRepository _repo_TipoEntregavel;
        private readonly IEntregavelRepository _repo;

        public EntregavelService(IUnitOfWork unitOfWork, IEntregavelRepository repo, ITipoEntregavelRepository repo_TipoEntregavel) {   
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repo_TipoEntregavel = repo_TipoEntregavel;
        }

        public async Task<List<EntregavelDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(entregavel => new EntregavelDto(entregavel));
        }
        

        public async Task<EntregavelDto> GetByIdAsync(EntregavelId id) {
            Entregavel entregavel = await this._repo.GetByIdAsync(id);

            return entregavel == null ? null : new EntregavelDto(entregavel);
        }


        public async Task<EntregavelDto> AddAsync(CreatingEntregavelDto dto)
        {
            var tipoEntregavel = await this._repo_TipoEntregavel.GetByIdAsync(new TipoEntregavelId(dto.TipoEntregavel.Id));

            if (tipoEntregavel == null){
                throw new BusinessRuleValidationException("Tipo de Entregável especificado não existe.");
            }

            var entregavel = new Entregavel(dto.Nome, dto.Descricao,dto.Data, tipoEntregavel);

            await this._repo.AddAsync(entregavel);

            await this._unitOfWork.CommitAsync();

            return new EntregavelDto (entregavel);
        }

        public async Task<EntregavelDto> UpdateAsync(EntregavelDto dto){
            var entregavel = await _repo.GetByIdAsync(new EntregavelId(dto.Id));
            if (entregavel == null)
                return null;

            var tipo = await _repo_TipoEntregavel.GetByIdAsync(new TipoEntregavelId(dto.TipoEntregavel.Id));
            if (tipo == null)
                throw new BusinessRuleValidationException("TipoEntregavel fornecido não existe.");

            // Atualiza os campos
            entregavel.AlterarAtributos(dto.Nome, dto.Descricao, dto.Data, tipo);

            await _unitOfWork.CommitAsync();
            return new EntregavelDto(entregavel);
        }


         public async Task<EntregavelDto> DeleteAsync(EntregavelId id)
        {
            Entregavel entregavel = await this._repo.GetByIdAsync(id);

            if (entregavel == null) return null;

            this._repo.Remove(entregavel);

            await this._unitOfWork.CommitAsync();

            return new EntregavelDto(entregavel);
        }
    }
}