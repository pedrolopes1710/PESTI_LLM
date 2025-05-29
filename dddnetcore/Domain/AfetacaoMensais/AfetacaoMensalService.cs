using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.AfetacaoPerfis;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Pessoas;
using dddnetcore.Infraestructure.Pessoas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoMensais
{
    public class AfetacaoMensalService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IAfetacaoMensalRepository _repo;
        public readonly IAfetacaoPerfilRepository _afetacaoPerfilRepo;
        public readonly ICargaMensalRepository _cargaMensalRepo;
        public readonly IPessoaRepository _pessoaRepo;

        public AfetacaoMensalService(IUnitOfWork unitOfWork, IAfetacaoMensalRepository repo, IAfetacaoPerfilRepository afetacaoPerfilRepo, ICargaMensalRepository cargaMensalRepo, IPessoaRepository pessoaRepo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._afetacaoPerfilRepo = afetacaoPerfilRepo;
            this._cargaMensalRepo = cargaMensalRepo;
            this._pessoaRepo = pessoaRepo;
        }

        public async Task<List<AfetacaoMensalDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(af => new AfetacaoMensalDto(af));
        }

        public async Task<AfetacaoMensalDto> GetByIdAsync(AfetacaoMensalId id) {
            AfetacaoMensal afetacaoMensal = await this._repo.GetByIdAsync(id);

            return afetacaoMensal == null ? null : new AfetacaoMensalDto(afetacaoMensal);
        }

        public async Task<AfetacaoMensalDto> AddAsync(CreatingAfetacaoMensalDto dto) {
            AfetacaoPerfil afetacaoPerfil = await this._afetacaoPerfilRepo.GetByIdAsync(new AfetacaoPerfilId(dto.AfetacaoPerfilId)) ?? throw new NullReferenceException("Not Found AfetacaoPerfil: " + dto.AfetacaoPerfilId);

            MesAno mesAno = new MesAno(dto.MesAno);
            Pessoa pessoa = await this._pessoaRepo.GetByIdAsync(afetacaoPerfil.PessoaId) ?? throw new NullReferenceException("Not Found Person: " + afetacaoPerfil.Pessoa.Id);

            CargaMensal cargaMensal = await this._cargaMensalRepo.GetByMesAnoAndPessoaAsync(mesAno, pessoa.Id) ?? throw new NullReferenceException("Not Found Month Schedule for month " + mesAno + " for person ID:" + pessoa.Id);
            if (!IsUnlocked(cargaMensal, pessoa)) {
                throw new Exception("The monthly schedule is locked for the person " + pessoa.Id + " for the month " + mesAno + "(payment already requested)");
            }

            AfetacaoMensal afetacaoMensal = new(
                new PMs(dto.PMs),
                afetacaoPerfil,
                cargaMensal
            );

            await this._repo.AddAsync(afetacaoMensal);
            await this._unitOfWork.CommitAsync();

            return new AfetacaoMensalDto(afetacaoMensal);
        }

        public async Task<AfetacaoMensalDto> UpdateAsync(AfetacaoMensalId id, EditingAfetacaoMensalDto dto) {
            AfetacaoMensal afetacaoMensal = await this._repo.GetByIdAsync(id) ?? throw new NullReferenceException("Not Found AfetacaoMensal: " + id);
            Pessoa pessoa = await this._pessoaRepo.GetByIdAsync(afetacaoMensal.AfetacaoPerfil.PessoaId) ?? throw new NullReferenceException("Not Found Person: " + afetacaoMensal.AfetacaoPerfil.Pessoa.Id);
            CargaMensal cargaMensal = afetacaoMensal.CargaMensal;

            if (!IsUnlocked(cargaMensal, pessoa)) {
                throw new Exception("The monthly schedule is locked for the person " + pessoa.Id + " for the month " + cargaMensal.MesAno + "(payment already requested)");
            }

            if (dto.PMs == 0) 
                return RemoveAsync(id).Result;

            afetacaoMensal.ChangePMs(new PMs(dto.PMs));

            await this._repo.UpdateAsync(afetacaoMensal);
            await this._unitOfWork.CommitAsync();

            return new AfetacaoMensalDto(afetacaoMensal);
        }

        public async Task<AfetacaoMensalDto> RemoveAsync(AfetacaoMensalId id) {
            AfetacaoMensal afetacaoMensal = await this._repo.GetByIdAsync(id) ?? throw new NullReferenceException("Not Found AfetacaoMensal: " + id);

            Pessoa pessoa = await this._pessoaRepo.GetByIdAsync(afetacaoMensal.AfetacaoPerfil.Pessoa.Id) ?? throw new NullReferenceException("Not Found Person: " + afetacaoMensal.AfetacaoPerfil.Pessoa.Id);
            CargaMensal cargaMensal = afetacaoMensal.CargaMensal;
            
            if (!IsUnlocked(cargaMensal, pessoa)) {
                throw new Exception("The monthly schedule is locked for the person " + pessoa.Id + " for the month " + cargaMensal.MesAno + "(payment already requested)");
            }

            this._repo.Remove(afetacaoMensal);
            await this._unitOfWork.CommitAsync();

            return new AfetacaoMensalDto(afetacaoMensal);
        }

        private static bool IsUnlocked(CargaMensal cargaMensal, Pessoa pessoa) {
            DateTime carga = cargaMensal.MesAno.Valor;
            DateTime pess = pessoa.UltimoPedidoPagamento.Value;
            return carga > pess;
        }
    }
}