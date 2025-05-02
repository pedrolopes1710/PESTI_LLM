using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using dddnetcore.Domain.Tarefas;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Projetos;
using dddnetcore.Domain.Entregaveis;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.AfetacaoPerfis;
using dddnetcore.Domain.Pessoas;

namespace DDDSample1.Domain.AfetacaoPerfis
{
    public class AfetacaoPerfilService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAfetacaoPerfilRepository _repo;
        private readonly IPessoaRepository _repoPessoa;
        private readonly IPerfilRepository _repoPerfil;

    


        public AfetacaoPerfilService(IUnitOfWork unitOfWork, IAfetacaoPerfilRepository repo, IPessoaRepository repoPessoa, IPerfilRepository repoPerfil) 
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoPessoa = repoPessoa;
            this._repoPerfil = repoPerfil;
        }

        public async Task<List<AfetacaoPerfilDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(afetacaoPerfil => new AfetacaoPerfilDto(afetacaoPerfil));
        }

        public async Task<AfetacaoPerfilDto> GetByIdAsync(AfetacaoPerfilId id) {
            AfetacaoPerfil afetacaoPerfil = await this._repo.GetByIdAsync(id);

            return afetacaoPerfil == null ? null : new AfetacaoPerfilDto(afetacaoPerfil);
        }

        public async Task<AfetacaoPerfilDto> AddAsync(CreatingAfetacaoPerfilDto dto)
        {
            Perfil? perfil = null;
            if (dto.PerfilId.HasValue)
            {
                perfil = await _repoPerfil.GetByIdAsync(new PerfilId(dto.PerfilId.Value));
            }            
            Pessoa? pessoa = null;
            if (dto.PerfilId.HasValue)
            {
                perfil = await _repoPerfil.GetByIdAsync(new PerfilId(dto.PerfilId.Value));
            }  
            var afetacaoPerfil = new AfetacaoPerfil(new DuracaoMes(dto.DuracaoMes), new PMsAprovados(dto.PMsAprovados), perfil, pessoa);

            await this._repo.AddAsync(afetacaoPerfil);
            await this._unitOfWork.CommitAsync();

            return new AfetacaoPerfilDto(afetacaoPerfil);
        }

        public async Task<AfetacaoPerfilDto> UpdateAsync(AfetacaoPerfilDto dto)
        {
            var afetacaoPerfil = await this._repo.GetByIdAsync(new AfetacaoPerfilId(dto.Id)); 

            if (afetacaoPerfil == null)
                return null;   

            // change all fields
            afetacaoPerfil.ChangeDuracaoMes(new DuracaoMes(dto.DuracaoMes));
            afetacaoPerfil.ChangePMsAprovados(new PMsAprovados(dto.PMsAprovados));
            
    
            await this._unitOfWork.CommitAsync();

            return new AfetacaoPerfilDto(afetacaoPerfil);
        }
               
        public async Task<AfetacaoPerfilDto> DeleteAsync(AfetacaoPerfilId id)
        {
            var afetacaoPerfil = await this._repo.GetByIdAsync(id); 

            if (afetacaoPerfil == null)
                return null;   

            this._repo.Remove(afetacaoPerfil);
            await this._unitOfWork.CommitAsync();

            return new AfetacaoPerfilDto(afetacaoPerfil);
        }
    }
}