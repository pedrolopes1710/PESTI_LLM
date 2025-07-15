using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dddnetcore.Controllers;
using dddnetcore.Domain.Pessoas;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.Projetos;
using dddnetcore.Domain.CargasMensais;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using DDDSample1.Domain.Shared;

public class PessoaControllerTests
{
    private readonly Mock<IPessoaRepository> _pessoaRepoMock = new();
    private readonly Mock<IContratoRepository> _contratoRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IProjetoRepository> _projetoRepoMock = new();
    private readonly Mock<ICargaMensalRepository> _cargaMensalRepoMock = new();

    private readonly PessoaService _pessoaService;
    private readonly PessoaController _controller;

    public PessoaControllerTests()
    {
        _pessoaService = new PessoaService(
            _unitOfWorkMock.Object,
            _pessoaRepoMock.Object,
            _contratoRepoMock.Object,
            _projetoRepoMock.Object,
            _cargaMensalRepoMock.Object
        );

        _controller = new PessoaController(_pessoaService);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfPessoa()
    {
        // Arrange
        var pessoa = new Pessoa(new NomePessoa("Ana"), new EmailPessoa("ana@gmail.com"), new PessoaCienciaId("CI1Y-JKI8-OP09"), new PessoaUltimoPedPagam(DateTime.Now), null);
        var lista = new List<Pessoa> { pessoa };

        _pessoaRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(lista);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsType<List<PessoaDto>>(okResult.Value);
        Assert.Single(returned);
        Assert.Equal("Ana", returned[0].Nome);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var pessoa = new Pessoa(new NomePessoa("Bruno"), new EmailPessoa("bruno@gmail.com"), new PessoaCienciaId("CI1Y-JKI8-OP09"), new PessoaUltimoPedPagam(DateTime.Now), null);

        _pessoaRepoMock.Setup(r => r.GetByIdAsync(new PessoaId(id))).ReturnsAsync(pessoa);

        // Act
        var result = await _controller.GetById(id.ToString());

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var dto = Assert.IsType<PessoaDto>(ok.Value);
        Assert.Equal("Bruno", dto.Nome);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        var id = Guid.NewGuid();
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(new PessoaId(id))).ReturnsAsync((Pessoa)null);

        // Act
        var result = await _controller.GetById(id.ToString());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task Create_ReturnsCreated_WhenValid()
    {
        // Arrange
        var dto = new CreatingPessoaDto
        {
            Nome = "Carla",
            Email = "carla@gmail.com",
            PessoaCienciaId = "CI1Y-JKI8-OP09",
            PessoaUltimoPedPagam = DateTime.Now
        };

        var novaPessoa = new Pessoa(
            new NomePessoa(dto.Nome),
            new EmailPessoa(dto.Email),
            new PessoaCienciaId(dto.PessoaCienciaId),
            new PessoaUltimoPedPagam(dto.PessoaUltimoPedPagam),
            null
        );

        _pessoaRepoMock.Setup(r => r.AddAsync(It.IsAny<Pessoa>())).ReturnsAsync(novaPessoa);

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
        var pessoaCriada = Assert.IsType<PessoaDto>(createdAt.Value);
        Assert.Equal("Carla", pessoaCriada.Nome);
    }

    [Fact]
    public async Task Update_ReturnsOk_WhenSuccess()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new EditingPessoaDto
        {
            Id = id.ToString(),
            Nome = "Diana",
            Email = "diana@gmail.com",
            PessoaUltimoPedPagam = DateTime.Now
        };

        var pessoaExistente = new Pessoa(new NomePessoa("Old"), new EmailPessoa("old@gmail.com"), new PessoaCienciaId("CI1Y-JKI8-OP09"), new PessoaUltimoPedPagam(DateTime.Now), null);
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(new PessoaId(id))).ReturnsAsync(pessoaExistente);

_unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

        // Act
        var result = await _controller.Update(id.ToString(), dto);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var updated = Assert.IsType<PessoaDto>(ok.Value);
        Assert.Equal("Diana", updated.Nome);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_OnIdMismatch()
    {
        // Arrange
        var dto = new EditingPessoaDto { Id = Guid.NewGuid().ToString() };
        var result = await _controller.Update(Guid.NewGuid().ToString(), dto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenSuccess()
    {
        // Arrange
        var id = Guid.NewGuid();
        var pessoa = new Pessoa(new NomePessoa("Ema"), new EmailPessoa("ema@gmail.com"), new PessoaCienciaId("CI1Y-JKI8-OP09"), new PessoaUltimoPedPagam(DateTime.Now), null);
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(new PessoaId(id))).ReturnsAsync(pessoa);
_unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

        // Act
        var result = await _controller.Delete(id.ToString());

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var dto = Assert.IsType<PessoaDto>(ok.Value);
        Assert.Equal("Ema", dto.Nome);
    }

    [Fact]
    public async Task Desativar_ReturnsOk_WhenSuccess()
    {
        // Arrange
        var id = Guid.NewGuid();
        var pessoa = new Pessoa(new NomePessoa("Filipe"), new EmailPessoa("filipe@gmail.com"), new PessoaCienciaId("CI1Y-JKI8-OP09"), new PessoaUltimoPedPagam(DateTime.Now), null);

        _pessoaRepoMock.Setup(r => r.GetByIdAsync(new PessoaId(id))).ReturnsAsync(pessoa);
_unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

        // Act
        var result = await _controller.DesativarPessoa(id.ToString());

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var dto = Assert.IsType<PessoaDto>(ok.Value);
        Assert.False(dto.Ativo);
    }
}
