using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.AfetacaoMensais;
using dddnetcore.Domain.AfetacaoPerfis;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.TabelaAfetacoes
{
    public class TabelaAfetacoesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPessoaRepository _pessoaRepo;
        private readonly IAfetacaoMensalRepository _afetacaoMensalRepo;
        private readonly IAfetacaoPerfilRepository _afetacaoPerfilRepo;

        public TabelaAfetacoesService(IUnitOfWork unitOfWork, IPessoaRepository pessoaRepository, IAfetacaoMensalRepository afetacaoMensalRepository, IAfetacaoPerfilRepository afetacaoPerfilRepository)
        {
            this._pessoaRepo = pessoaRepository;
            this._unitOfWork = unitOfWork;
            this._afetacaoMensalRepo = afetacaoMensalRepository;
            this._afetacaoPerfilRepo = afetacaoPerfilRepository;
        }

        public async Task<TabelaAfetacoesDto> GenerateAsync(Guid pessoaId) {

            Pessoa pessoa = await this._pessoaRepo.GetByIdAsync(new PessoaId(pessoaId)) ?? throw new NullReferenceException("Not Found Person: " + pessoaId);
            List<CargaMensal> cargasMensais = pessoa.CargasMensais;
            List<AfetacaoPerfil> afetacaoPerfils = await this._afetacaoPerfilRepo.GetByPessoaIdAsync(pessoa.Id);
            List<Perfil> perfils = [];
            foreach (AfetacaoPerfil afetacaoPerfil in afetacaoPerfils)
            {
                perfils.Add(afetacaoPerfil.Perfil);
            }
            List<AfetacaoMensal> afetacaoMensais = [];
            foreach (CargaMensal cargaMensal in cargasMensais)
            {
                afetacaoMensais.AddRange(await this._afetacaoMensalRepo.GetByCargaMensalIdAsync(cargaMensal.Id));
            }

            return new TabelaAfetacoesDto(pessoa, afetacaoMensais, afetacaoPerfils, perfils, cargasMensais);
        }
    }
}