using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.TiposEntregavel
{
    public class TipoEntregavelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoEntregavelRepository _repo;

        public TipoEntregavelService(IUnitOfWork unitOfWork, ITipoEntregavelRepository repo) {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<TipoEntregavelDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(tipo => new TipoEntregavelDto(tipo));
        }
        
        public async Task<TipoEntregavelDto> GetByIdAsync(TipoEntregavelId id) {
            TipoEntregavel tipo = await this._repo.GetByIdAsync(id);

            return tipo == null ? null : new TipoEntregavelDto(tipo);
        }


        public async Task<TipoEntregavelDto> AddAsync(CreatingTipoEntregavelDto dto)
        {
            var tipo = new TipoEntregavel(dto.Nome);

            await this._repo.AddAsync(tipo);

            await this._unitOfWork.CommitAsync();

            return new TipoEntregavelDto (tipo);
        }

        public async Task<TipoEntregavelDto> UpdateAsync(TipoEntregavelDto dto){
            var tipo = await _repo.GetByIdAsync(new TipoEntregavelId(dto.Id));
            if (tipo == null)
                return null;

            // Atualiza os campos
            tipo.AlterarAtributos(dto.Nome);

            await _unitOfWork.CommitAsync();
            return new TipoEntregavelDto(tipo);
        }

         public async Task<TipoEntregavelDto> DeleteAsync(TipoEntregavelId id)
        {
            TipoEntregavel tipo = await this._repo.GetByIdAsync(id);

            if (tipo == null) return null;

            this._repo.Remove(tipo);

            await this._unitOfWork.CommitAsync();

            return new TipoEntregavelDto(tipo);
        }
    }
}