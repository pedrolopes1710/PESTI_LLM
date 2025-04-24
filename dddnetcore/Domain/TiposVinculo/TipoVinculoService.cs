using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.TiposVinculo
{
    public class TipoVinculoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoVinculoRepository _repo;

        public TipoVinculoService(IUnitOfWork unitOfWork, ITipoVinculoRepository repo) {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<TipoVinculoDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(tipo => new TipoVinculoDto(tipo));
        }
        
        public async Task<TipoVinculoDto> GetByIdAsync(TipoVinculoId id) {
            TipoVinculo tipo = await this._repo.GetByIdAsync(id);

            return tipo == null ? null : new TipoVinculoDto(tipo);
        }


        public async Task<TipoVinculoDto> AddAsync(CreatingTipoVinculoDto dto)
        {
            var tipo = new TipoVinculo(dto.Nome);

            await this._repo.AddAsync(tipo);

            await this._unitOfWork.CommitAsync();

            return new TipoVinculoDto (tipo);
        }

        /*public async Task<TipoVinculoDto> UpdateAsync(TipoVinculoDto dto){
            var tipo = await _repo.GetByIdAsync(new TipoVinculoId(dto.Id));
            if (tipo == null)
                return null;

            // Atualiza os campos
            tipo.AlterarAtributos(dto.Nome);

            await _unitOfWork.CommitAsync();
            return new TipoVinculoDto(tipo);
        }*/

         public async Task<TipoVinculoDto> DeleteAsync(TipoVinculoId id)
        {
            TipoVinculo tipo = await this._repo.GetByIdAsync(id);

            if (tipo == null) return null;

            this._repo.Remove(tipo);

            await this._unitOfWork.CommitAsync();

            return new TipoVinculoDto(tipo);
        }
    }
}