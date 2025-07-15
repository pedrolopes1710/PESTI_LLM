using System;
using Xunit;
using dddnetcore.Domain.Pessoas;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.CargasMensais;
using DDDSample1.Domain.Shared;
using Moq;

namespace dddnetcore.Tests.Domain.Pessoas
{
    public class PessoaTests
    {
        private Pessoa CriarPessoa()
        {
            var nome = new NomePessoa("João Silva");
            var email = new EmailPessoa("joao@isep.ipp.pt");
            var cienciaId = new PessoaCienciaId("ERF5-768H-UIHK");
            var ultimoPedidoPagamento = new PessoaUltimoPedPagam(DateTime.Now);
            var salario = new SalarioMensalContrato(2000);
            var dataInicio = new DataInicioContrato(DateTime.Today.AddMonths(-6));
            var dataFim = new DataFimContrato(DateTime.Today.AddMonths(6));

            var contrato = new Contrato(TipoContrato.Bolseiro, salario, dataInicio, dataFim);

            return new Pessoa(nome, email, cienciaId, ultimoPedidoPagamento, contrato);
        }

        [Fact]
        public void CriarPessoa_DeveTerContratoEAtiva()
        {
            var pessoa = CriarPessoa();

            Assert.NotNull(pessoa.Contrato);
            Assert.True(pessoa.Ativo);
            Assert.Equal("João Silva", pessoa.Nome.Value);
        }

        [Fact]
        public void AlterarNome_DeveAtualizarValor()
        {
            var pessoa = CriarPessoa();
            var novoNome = new NomePessoa("Maria Oliveira");

            pessoa.AlterarNome(novoNome);

            Assert.Equal("Maria Oliveira", pessoa.Nome.Value);
        }

        [Fact]
        public void AdicionarCargaMensal_DeveAcrescentarNaLista()
        {
            var pessoa = CriarPessoa();

            var carga = new CargaMensal(
                new JornadaDiaria(8),
                new DiasUteisTrabalhaveis(22),
                new FeriasBaixasLicencasFaltas(2),
                new SalarioBase(1200),
                new MesAno(new DateTime(2024, 4, 1)),
                new TaxaSocialUnica(11))
            {
                // Supondo que AlterarPessoaId é necessário
            };
            carga.AlterarPessoaId(pessoa.Id);

            pessoa.AdicionarCargaMensal(carga);

            Assert.Single(pessoa.CargasMensais);
        }


        [Fact]
        public void AlterarEmail_DeveAtualizarValor()
        {
            var pessoa = CriarPessoa();
            var novoEmail = new EmailPessoa("maria@email.com");

            pessoa.AlterarEmail(novoEmail);

            Assert.Equal("maria@email.com", pessoa.Email.Value);
        }

        [Fact]
        public void DesativarPessoa_DeveMudarEstado()
        {
            var pessoa = CriarPessoa();

            pessoa.Desativar();

            Assert.False(pessoa.Ativo);
        }

        [Fact]
        public void ReativarPessoa_DeveMudarEstado()
        {
            var pessoa = CriarPessoa();
            pessoa.Desativar();

            pessoa.Reativar();

            Assert.True(pessoa.Ativo);
        }

        [Fact]
        public void RemoverContrato_DeveDefinirNull()
        {
            var pessoa = CriarPessoa();

            pessoa.RemoverContrato();

            Assert.Null(pessoa.Contrato);
            Assert.Null(pessoa.ContratoId);
        }

    }
    }

