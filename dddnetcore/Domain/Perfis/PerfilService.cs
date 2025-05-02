using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.AfetacaoMensais;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Tarefas;
using dddnetcore.Domain.TiposVinculo;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Perfis
{
    public class PerfilService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPerfilRepository _repo;
        private readonly IAtividadeRepository _atividadeRepo;

        private readonly ITipoVinculoRepository _tipoVinculoRepo;

        public PerfilService(IUnitOfWork unitOfWork, IPerfilRepository repo, IAtividadeRepository atividadeRepo, ITipoVinculoRepository tipoVinculoRepo) {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._atividadeRepo = atividadeRepo;
            this._tipoVinculoRepo = tipoVinculoRepo;
        }

        public async Task<List<PerfilDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(tarefa => new PerfilDto(tarefa));
        }
        
        public async Task<PerfilDto> GetByIdAsync(PerfilId id) {
            Perfil perfil = await this._repo.GetByIdAsync(id);

            return perfil == null ? null : new PerfilDto(perfil);
        }


        public async Task<PerfilDto> AddAsync(CreatingPerfilDto dto)
        {
        
            var tipoVinculo = await this._tipoVinculoRepo.GetByIdAsync(new TipoVinculoId(dto.TipoVinculoId));
            var perfil = new Perfil(dto.PMs, dto.Descricao, tipoVinculo);

            await this._repo.AddAsync(perfil);
            await this._unitOfWork.CommitAsync();

            return new PerfilDto(perfil);
        }

        public async Task<PerfilDto> UpdateAsync(PerfilDto dto)
        {
            Perfil perfil = await this._repo.GetByIdAsync(new PerfilId(dto.Id)); 

            if (perfil == null)
                return null;   

            // change all field
            perfil.ChangeDescricao(new DescricaoPerfil(dto.Descricao));
            perfil.ChangePMs(new PMsTotais(dto.PMs));
            
            await this._unitOfWork.CommitAsync();

            return new PerfilDto(perfil);
            
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


         public async Task<PerfilDto> DeleteAsync(Guid id)
        {
            Perfil perfil = await this._repo.GetByIdAsync(new PerfilId(id)); 

            if (perfil == null)
                return null;   

            this._repo.Remove(perfil);
            await this._unitOfWork.CommitAsync();

            return new PerfilDto(perfil);
        }
    }
}