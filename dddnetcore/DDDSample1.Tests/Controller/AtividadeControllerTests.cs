using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DDDSample1.Controllers;
using DDDSample1.Domain.Atividades;
using dddnetcore.Domain.Tarefas;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Entregaveis;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Atividades;
using DDDSample1.Domain.Shared;

public class AtividadesControllerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IAtividadeRepository> _repoMock = new();
    private readonly Mock<ITarefaRepository> _repoTarefaMock = new();
    private readonly Mock<IOrcamentoRepository> _repoOrcamentoMock = new();
    private readonly Mock<IEntregavelRepository> _repoEntregavelMock = new();
    private readonly Mock<IPerfilRepository> _repoPerfilMock = new();

    private AtividadesController CreateController()
    {
        var service = new AtividadeService(
            _unitOfWorkMock.Object,
            _repoMock.Object,
            _repoTarefaMock.Object,
            _repoOrcamentoMock.Object,
            _repoEntregavelMock.Object,
            _repoPerfilMock.Object
        );

        return new AtividadesController(service);
    }

    [Fact]
    public async Task GetAll_ReturnsOkList()
    {
        // Arrange
        var atividades = new List<Atividade>
        {
            new(new DataFimAtividade(DateTime.Now.AddDays(1)),
                new DataInicioAtividade(DateTime.Now),
                new DescricaoAtividade("Descricao Teste"),
                new NomeAtividade("Nome Teste"))
        };

        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(atividades);

        var controller = CreateController();

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = Assert.IsType<ActionResult<IEnumerable<AtividadeDto>>>(result);
        Assert.Single(okResult.Value!);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenNotExists()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<AtividadeId>())).ReturnsAsync((Atividade)null!);

        var controller = CreateController();

        // Act
        var result = await controller.GetById(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAt_WhenValid()
    {
        // Arrange
        var dto = new CreatingAtividadeDto
        {
            DataInicioAtividade = DateTime.Today,
            DataFimAtividade = DateTime.Today.AddDays(1),
            DescricaoAtividade = "Teste",
            NomeAtividade = "Teste Nome"
        };

        var controller = CreateController();

        // Act
        var result = await controller.Create(dto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var value = Assert.IsType<AtividadeDto>(createdResult.Value);
        Assert.Equal("Teste Nome", value.NomeAtividade);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdsDiffer()
    {
        // Arrange
        var controller = CreateController();
        var dto = new AtividadeDto { Id = Guid.NewGuid() };

        // Act
        var result = await controller.Update(Guid.NewGuid(), dto);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task HardDelete_ReturnsNotFound_WhenNotExists()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<AtividadeId>())).ReturnsAsync((Atividade)null!);
        var controller = CreateController();

        // Act
        var result = await controller.HardDelete(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
