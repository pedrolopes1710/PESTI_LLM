using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.Projetos;
using dddnetcore.Domain.CargasMensais;
using DDDSample1.Domain.Shared;
using FluentAssertions;
using Moq;
using Xunit;

public class PessoaServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPessoaRepository> _pessoaRepoMock;
    private readonly Mock<IContratoRepository> _contratoRepoMock;
    private readonly Mock<IProjetoRepository> _projetoRepoMock;
    private readonly Mock<ICargaMensalRepository> _cargaMensalRepoMock;

    private readonly PessoaService _service;

    public PessoaServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _pessoaRepoMock = new Mock<IPessoaRepository>();
        _contratoRepoMock = new Mock<IContratoRepository>();
        _projetoRepoMock = new Mock<IProjetoRepository>();
        _cargaMensalRepoMock = new Mock<ICargaMensalRepository>();

        _service = new PessoaService(
            _unitOfWorkMock.Object,
            _pessoaRepoMock.Object,
            _contratoRepoMock.Object,
            _projetoRepoMock.Object,
            _cargaMensalRepoMock.Object
        );
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnPessoaDto_WhenPessoaExists()
    {
        // Arrange
        var pessoaId = new PessoaId(Guid.NewGuid());
        var pessoa = new Pessoa(
            new NomePessoa("João Silva"),
            new EmailPessoa("joao@gmail.com"),
            new PessoaCienciaId("CREG-567G-JKI8"),
            new PessoaUltimoPedPagam(DateTime.Today),
            null);

        // Set the ID explicitly (since ctor generates new Id)
        typeof(Entity<PessoaId>).GetProperty("Id").SetValue(pessoa, pessoaId);

        var contrato = new Contrato(
            TipoContrato.Bolseiro,
            new SalarioMensalContrato(2000),
            new DataInicioContrato(DateTime.Today.AddMonths(-6)),
            new DataFimContrato(DateTime.Today.AddMonths(6)));

        var projetos = new List<Projeto>
        {
            new Projeto("Projeto A", "Descrição A", new PessoaId(Guid.NewGuid()))
        };

        var cargas = new List<CargaMensal>();

        _pessoaRepoMock.Setup(r => r.GetByIdAsync(pessoaId)).ReturnsAsync(pessoa);
        _contratoRepoMock.Setup(r => r.GetByPessoaIdAsync(pessoaId)).ReturnsAsync(contrato);
        _pessoaRepoMock.Setup(r => r.GetProjetosByPessoaIdAsync(pessoaId)).ReturnsAsync(projetos);
        _cargaMensalRepoMock.Setup(r => r.GetByPessoaIdAsync(pessoaId)).ReturnsAsync(cargas);

        // Act
        var result = await _service.GetByIdAsync(pessoaId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(pessoaId.AsGuid());
        result.Nome.Should().Be("João Silva");
        result.Email.Should().Be("joao@gmail.com");
        result.Contrato.Should().NotBeNull();
        result.Contrato.Salario.Should().Be(2000);
        result.Projetos.Should().HaveCount(1);
        result.CargasMensais.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrow_WhenPessoaNotFound()
    {
        // Arrange
        var pessoaId = new PessoaId(Guid.NewGuid());

        _pessoaRepoMock.Setup(r => r.GetByIdAsync(pessoaId)).ReturnsAsync((Pessoa)null);

        // Act
        Func<Task> act = async () => await _service.GetByIdAsync(pessoaId);

        // Assert
        await act.Should().ThrowAsync<NullReferenceException>()
            .WithMessage($"Pessoa não encontrada com o ID: {pessoaId}");
    }

    [Fact]
    public async Task AddAsync_ShouldAddPessoa_WhenContratoIsValid()
    {
        // Arrange
        var contratoId = Guid.NewGuid().ToString();
        var contratoIdObj = new ContratoId(contratoId);

        var dto = new CreatingPessoaDto
        {
            Nome = "Maria",
            Email = "maria@gmail.com",
            PessoaCienciaId = "CI99-785T-87YT",
            PessoaUltimoPedPagam = DateTime.Today,
            ContratoId = contratoId
        };

        var contrato = new Contrato(
            TipoContrato.Bolseiro,
            new SalarioMensalContrato(3000),
            new DataInicioContrato(DateTime.Today.AddMonths(-1)),
            new DataFimContrato(DateTime.Today.AddMonths(11))
        );

        // 🔧 Força o ID via reflexão
        typeof(Contrato)
            .GetProperty("Id")
            ?.SetValue(contrato, contratoIdObj);

        _contratoRepoMock
            .Setup(r => r.GetByIdAsync(It.Is<ContratoId>(id => id.AsString() == contratoId)))
            .ReturnsAsync(contrato);

        _pessoaRepoMock
            .Setup(r => r.GetByContratoIdAsync(It.Is<ContratoId>(id => id.AsString() == contratoId)))
            .ReturnsAsync((Pessoa)null);

        _pessoaRepoMock
            .Setup(r => r.AddAsync(It.IsAny<Pessoa>()))
            .ReturnsAsync((Pessoa p) => p);

        _unitOfWorkMock
            .Setup(u => u.CommitAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _service.AddAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.Nome.Should().Be("Maria");
        result.Contrato.Should().NotBeNull();
        result.Contrato.Id.Should().Be(contratoId);
    }
}