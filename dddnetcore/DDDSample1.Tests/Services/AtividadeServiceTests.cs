using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Atividades;
using dddnetcore.Domain.Tarefas;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Projetos;
using dddnetcore.Domain.Entregaveis;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Atividades;

public class AtividadeServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IAtividadeRepository> _repoMock = new();
    private readonly Mock<ITarefaRepository> _repoTarefaMock = new();
    private readonly Mock<IOrcamentoRepository> _repoOrcamentoMock = new();
    private readonly Mock<IEntregavelRepository> _repoEntregavelMock = new();
    private readonly Mock<IPerfilRepository> _repoPerfilMock = new();

    private AtividadeService CreateService() =>
        new(_unitOfWorkMock.Object, _repoMock.Object, _repoTarefaMock.Object, _repoOrcamentoMock.Object, _repoEntregavelMock.Object, _repoPerfilMock.Object);

    [Fact]
    public async Task GetAllAsync_ReturnsListOfDtos()
    {
        var atividades = new List<Atividade>
        {
            new(new DataFimAtividade(new System.DateTime(2025, 12, 31)), new DataInicioAtividade(new System.DateTime(2025, 1, 1)), new DescricaoAtividade("desc"), new NomeAtividade("nome"))
        };

        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(atividades);

        var service = CreateService();
        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal("desc", result[0].DescricaoAtividade);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsDto_WhenExists()
    {
        var atividade = new Atividade(
            new DataFimAtividade(new System.DateTime(2025, 12, 31)),
            new DataInicioAtividade(new System.DateTime(2025, 1, 1)),
            new DescricaoAtividade("desc"),
            new NomeAtividade("nome"));
        var id = atividade.Id;

        _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(atividade);

        var service = CreateService();
        var result = await service.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equal("nome", result.NomeAtividade);
    }

    [Fact]
    public async Task AddAsync_CreatesAtividadeAndCommits()
    {
        var dto = new CreatingAtividadeDto
        {
            DataFimAtividade = new System.DateTime(2025, 12, 31),
            DataInicioAtividade = new System.DateTime(2025, 1, 1),
            DescricaoAtividade = "descricao",
            NomeAtividade = "nome"
        };

        var service = CreateService();
        var result = await service.AddAsync(dto);

        _repoMock.Verify(r => r.AddAsync(It.IsAny<Atividade>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.AtLeastOnce);
        Assert.Equal("nome", result.NomeAtividade);
    }

    [Fact]
    public async Task UpdateAsync_ChangesFields_WhenExists()
    {
        var atividade = new Atividade(
            new DataFimAtividade(new System.DateTime(2025, 12, 31)),
            new DataInicioAtividade(new System.DateTime(2025, 1, 1)),
            new DescricaoAtividade("old"),
            new NomeAtividade("oldname"));
        var dto = new AtividadeDto(atividade)
        {
            DescricaoAtividade = "nova descricao",
            NomeAtividade = "novo nome"
        };

        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<AtividadeId>())).ReturnsAsync(atividade);

        var service = CreateService();
        var result = await service.UpdateAsync(dto);

        Assert.Equal("nova descricao", result.DescricaoAtividade);
        Assert.Equal("novo nome", result.NomeAtividade);
    }

    [Fact]
    public async Task DeleteAsync_RemovesAtividade_WhenExists()
    {
        var atividade = new Atividade(
            new DataFimAtividade(new System.DateTime(2025, 12, 31)),
            new DataInicioAtividade(new System.DateTime(2025, 1, 1)),
            new DescricaoAtividade("desc"),
            new NomeAtividade("nome"));
        var id = atividade.Id;

        _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(atividade);

        var service = CreateService();
        var result = await service.DeleteAsync(id);

        _repoMock.Verify(r => r.Remove(atividade), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        Assert.Equal("desc", result.DescricaoAtividade);
    }
    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenDescricaoIsNull()
    {
        var dto = new CreatingAtividadeDto
        {
            DataInicioAtividade = new DateTime(2025, 1, 1),
            DataFimAtividade = new DateTime(2025, 12, 31),
            DescricaoAtividade = null!, // inválido
            NomeAtividade = "nome"
        };

        var service = CreateService();

        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => service.AddAsync(dto));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenNomeIsNull()
    {
        var dto = new CreatingAtividadeDto
        {
            DataInicioAtividade = new DateTime(2025, 1, 1),
            DataFimAtividade = new DateTime(2025, 12, 31),
            DescricaoAtividade = "descricao",
            NomeAtividade = null! // inválido
        };

        var service = CreateService();

        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => service.AddAsync(dto));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenDescricaoIsEmpty()
    {
        var dto = new CreatingAtividadeDto
        {
            DataInicioAtividade = new DateTime(2025, 1, 1),
            DataFimAtividade = new DateTime(2025, 12, 31),
            DescricaoAtividade = "", // inválido
            NomeAtividade = "nome"
        };

        var service = CreateService();

        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => service.AddAsync(dto));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenNomeIsEmpty()
    {
        var dto = new CreatingAtividadeDto
        {
            DataInicioAtividade = new DateTime(2025, 1, 1),
            DataFimAtividade = new DateTime(2025, 12, 31),
            DescricaoAtividade = "descricao",
            NomeAtividade = "" // inválido
        };

        var service = CreateService();

        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => service.AddAsync(dto));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenDataInicioIsMinValue()
    {
        var dto = new CreatingAtividadeDto
        {
            DataInicioAtividade = DateTime.MinValue, // inválido
            DataFimAtividade = new DateTime(2025, 12, 31),
            DescricaoAtividade = "descricao",
            NomeAtividade = "nome"
        };

        var service = CreateService();

        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => service.AddAsync(dto));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenDataFimIsBeforeDataInicio()
    {
        var dto = new CreatingAtividadeDto
        {
            DataInicioAtividade = new DateTime(2025, 12, 31),
            DataFimAtividade = new DateTime(2025, 1, 1), // inválido
            DescricaoAtividade = "descricao",
            NomeAtividade = "nome"
        };

        var service = CreateService();

        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => service.AddAsync(dto));
    }
    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenNomeIsNull()
    {
        var atividade = new Atividade(
            new DataFimAtividade(new DateTime(2025, 12, 31)),
            new DataInicioAtividade(new DateTime(2025, 1, 1)),
            new DescricaoAtividade("descricao"),
            new NomeAtividade("nome"));

        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<AtividadeId>())).ReturnsAsync(atividade);

        var dto = new AtividadeDto(atividade)
        {
            NomeAtividade = null
        };

        var service = CreateService();

        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => service.UpdateAsync(dto));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenDescricaoIsNull()
    {
        var atividade = new Atividade(
            new DataFimAtividade(new DateTime(2025, 12, 31)),
            new DataInicioAtividade(new DateTime(2025, 1, 1)),
            new DescricaoAtividade("descricao"),
            new NomeAtividade("nome"));

        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<AtividadeId>())).ReturnsAsync(atividade);

        var dto = new AtividadeDto(atividade)
        {
            DescricaoAtividade = null
        };

        var service = CreateService();

        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => service.UpdateAsync(dto));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenDataInicioIsMinValue()
    {
        var atividade = new Atividade(
            new DataFimAtividade(new DateTime(2025, 12, 31)),
            new DataInicioAtividade(new DateTime(2025, 1, 1)),
            new DescricaoAtividade("descricao"),
            new NomeAtividade("nome"));

        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<AtividadeId>())).ReturnsAsync(atividade);

        var dto = new AtividadeDto(atividade)
        {
            DataInicioAtividade = DateTime.MinValue
        };

        var service = CreateService();

        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => service.UpdateAsync(dto));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenDataFimIsBeforeDataInicio()
    {
        var atividade = new Atividade(
            new DataFimAtividade(new DateTime(2025, 12, 31)),
            new DataInicioAtividade(new DateTime(2025, 1, 1)),
            new DescricaoAtividade("descricao"),
            new NomeAtividade("nome"));

        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<AtividadeId>())).ReturnsAsync(atividade);

        var dto = new AtividadeDto(atividade)
        {
            DataInicioAtividade = new DateTime(2025, 12, 1),
            DataFimAtividade = new DateTime(2025, 1, 1)
        };

        var service = CreateService();

        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => service.UpdateAsync(dto));
    }

    
}
