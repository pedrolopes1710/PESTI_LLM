using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.AfetacaoPerfis;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Pessoas;
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

        public AfetacaoMensalService(IUnitOfWork unitOfWork, IAfetacaoMensalRepository repo, IAfetacaoPerfilRepository afetacaoPerfilRepo, ICargaMensalRepository cargaMensalRepo) {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._afetacaoPerfilRepo = afetacaoPerfilRepo;
            this._cargaMensalRepo = cargaMensalRepo;
        }

        public async Task<List<AfetacaoMensalDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(af => new AfetacaoMensalDto(af));
        }

        public async Task<AfetacaoMensalDto> GetByIdAsync(AfetacaoMensalId id) {
            AfetacaoMensal afetacaoMensal = await this._repo.GetByIdAsync(id);

            return afetacaoMensal == null ? null : new AfetacaoMensalDto(afetacaoMensal);
        }

        //TODO: testar quando possível
        public async Task<AfetacaoMensalDto> AddAsync(CreatingAfetacaoMensalDto dto) {
            AfetacaoPerfil afetacaoPerfil = await this._afetacaoPerfilRepo.GetByIdAsync(new AfetacaoPerfilId(dto.AfetacaoPerfilId)) ?? throw new NullReferenceException("Not Found AfetacaoPerfil: " + dto.AfetacaoPerfilId);
            
            MesAno mesAno = new MesAno(dto.MesAno);
            Pessoa pessoa = await this._pessoaRepo.GetByIdAsync(afetacaoPerfil.Pessoa.Id) ?? throw new NullReferenceException("Not Found Person: " + afetacaoPerfil.Pessoa.Id);
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

        //TODO acabar de implementar o resto dos metodos

        private bool IsUnlocked(CargaMensal cargaMensal, Pessoa pessoa) {
            DateTime carga = cargaMensal.MesAno.Valor;
            DateTime pess = pessoa.UltimoPedidoPagamento.Value;
            return carga > pess;
        }
    }
}