using System;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.CargasMensais
{
    public class CargaMensal : Entity<CargaMensalId>, IAggregateRoot
    {
        public JornadaDiaria JornadaDiaria { get; private set; }
        public DiasUteisTrabalhaveis DiasUteis { get; private set; }
        public FeriasBaixasLicencasFaltas Ausencias { get; private set; }
        public SalarioBase SalarioBase { get; private set; }
        public MesAno MesAno { get; private set; }
        public TaxaSocialUnica TSU { get; private set; }
        public PessoaId PessoaId { get; private set; }



        private CargaMensal() { }

        public CargaMensal(
            JornadaDiaria jornadaDiaria,
            DiasUteisTrabalhaveis diasUteis,
            FeriasBaixasLicencasFaltas ausencias,
            SalarioBase salarioBase,
            MesAno mesAno,
            TaxaSocialUnica tsu)
        {
            this.Id = new CargaMensalId(Guid.NewGuid());
            this.JornadaDiaria = jornadaDiaria ?? throw new ArgumentNullException(nameof(jornadaDiaria));
            this.DiasUteis = diasUteis ?? throw new ArgumentNullException(nameof(diasUteis));
            this.Ausencias = ausencias ?? throw new ArgumentNullException(nameof(ausencias));
            this.SalarioBase = salarioBase ?? throw new ArgumentNullException(nameof(salarioBase));
            this.MesAno = mesAno ?? throw new ArgumentNullException(nameof(mesAno));
            this.TSU = tsu ?? throw new ArgumentNullException(nameof(tsu));
        }

        public void AlterarJornada(JornadaDiaria novaJornada)
        {
            this.JornadaDiaria = novaJornada ?? throw new ArgumentNullException(nameof(novaJornada));
        }

        public void AlterarDiasUteis(DiasUteisTrabalhaveis novosDias)
        {
            this.DiasUteis = novosDias ?? throw new ArgumentNullException(nameof(novosDias));
        }

        public void AlterarAusencias(FeriasBaixasLicencasFaltas novasAusencias)
        {
            this.Ausencias = novasAusencias ?? throw new ArgumentNullException(nameof(novasAusencias));
        }

        public void AlterarSalarioBase(SalarioBase novoSalario)
        {
            this.SalarioBase = novoSalario ?? throw new ArgumentNullException(nameof(novoSalario));
        }

        public void AlterarMesAno(MesAno novoMesAno)
        {
            this.MesAno = novoMesAno ?? throw new ArgumentNullException(nameof(novoMesAno));
        }

        public void AlterarTSU(TaxaSocialUnica novaTSU)
        {
            this.TSU = novaTSU ?? throw new ArgumentNullException(nameof(novaTSU));
        }
        
        public void AlterarPessoaId(PessoaId pessoaId)
        {
            if (pessoaId == null)
                throw new BusinessRuleValidationException("PessoaId cannot be null.");
            this.PessoaId = pessoaId;
        }        

        public override string ToString()
        {
            return $"CargaMensal: {Id}, Jornada: {JornadaDiaria.Valor}, Dias Úteis: {DiasUteis.Valor}, Ausências: {Ausencias.Dias}, Salário: {SalarioBase.Valor}, Mês/Ano: {MesAno.Valor.ToString("MM/yyyy")}, TSU: {TSU.Valor}";
        }
    }
}
