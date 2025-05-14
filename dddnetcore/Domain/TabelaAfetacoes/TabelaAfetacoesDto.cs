using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.AfetacaoMensais;
using dddnetcore.Domain.AfetacaoPerfis;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Pessoas;

namespace dddnetcore.Domain.TabelaAfetacoes
{
    public class TabelaAfetacoesDto
    {
        public PessoaDto Pessoa { get; set; }
        public List<AfetacaoMensalDto> AfetacoesMensais { get; set; }
        public List<AfetacaoPerfilDto> AfetacoesPerfis { get; set; }
        public List<PerfilDto> Perfis { get; set; }
        public List<CargaMensalDto> CargasMensais { get; set; }

        public TabelaAfetacoesDto(Pessoa pessoa, List<AfetacaoMensal> afetacoesMensais, List<AfetacaoPerfil> afetacoesPerfis, List<Perfil> perfis, List<CargaMensal> cargasMensais)
        {
            Pessoa = new PessoaDto(pessoa);
            AfetacoesMensais = afetacoesMensais.ConvertAll(af => new AfetacaoMensalDto(af));
            AfetacoesPerfis = afetacoesPerfis.ConvertAll(af => new AfetacaoPerfilDto(af));
            Perfis = perfis.ConvertAll(p => new PerfilDto(p));
            CargasMensais = cargasMensais.ConvertAll(c => new CargaMensalDto(c));
        }
    }
}