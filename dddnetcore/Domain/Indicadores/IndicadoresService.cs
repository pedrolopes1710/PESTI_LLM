using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Indicadores;
using dddnetcore.Domain.Projetos;
using DDDSample1.Domain.Shared;

public class IndicadorService
{
    private readonly IIndicadorRepository _repo;
    private readonly IProjetoRepository _projetoRepository;  // Certifique-se de que tem o repositório de Projetos
    private readonly IUnitOfWork _unitOfWork;

    public IndicadorService(IIndicadorRepository repo, IProjetoRepository projetoRepository, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _projetoRepository = projetoRepository;  // Adicionando o repositório de Projeto
        _unitOfWork = unitOfWork;
    }

    public async Task<IndicadorDTO> CriarAsync(CriarIndicadorDTO dto)
    {
        // Verificando se o ProjetoId existe
        var projeto = await _projetoRepository.GetByIdAsync(new ProjetoId(dto.ProjetoId)); // Criando ProjetoId aqui
        if (projeto == null)
        {
            throw new BusinessRuleValidationException("Projeto não encontrado.");
        }

        // Criando o indicador com o ProjetoId
        var indicador = new Indicador(dto.Nome, dto.ValorAtual, dto.ValorMaximo, new ProjetoId(dto.ProjetoId)); // Criando ProjetoId aqui

        // Adicionando o indicador no repositório
        await _repo.AddAsync(indicador);
    
        // Commitando a transação
        await _unitOfWork.CommitAsync();

        // Retornando o DTO com os dados do novo indicador
        return new IndicadorDTO
        {
            Id = indicador.Id.AsGuid(),
            Nome = dto.Nome,
            ValorAtual = dto.ValorAtual,
            ValorMaximo = dto.ValorMaximo
        };
    }


    public async Task<List<IndicadorDTO>> GetAllAsync()
    {
        var indicadores = await _repo.GetAllAsync();
        return indicadores.Select(i => new IndicadorDTO
        {
            Id = i.Id.AsGuid(),
            Nome = i.Nome.Valor,
            ValorAtual = i.ValorAtual.Valor,
            ValorMaximo = i.ValorMaximo.Valor
        }).ToList();
    }

    public async Task<IndicadorDTO> GetByIdAsync(Guid id)
    {
        var indicador = await _repo.GetByIdAsync(new IndicadorId(id));
        if (indicador == null) return null;

        return new IndicadorDTO
        {
            Id = indicador.Id.AsGuid(),
            Nome = indicador.Nome.Valor,
            ValorAtual = indicador.ValorAtual.Valor,
            ValorMaximo = indicador.ValorMaximo.Valor
        };
    }

    public async Task<IndicadorDTO> AtualizarAsync(Guid id, double novoValor)
    {
        var indicador = await _repo.GetByIdAsync(new IndicadorId(id));
        if (indicador == null) return null;

        indicador.AtualizarValorAtual(novoValor);
        await _unitOfWork.CommitAsync();

        return new IndicadorDTO
        {
            Id = indicador.Id.AsGuid(),
            Nome = indicador.Nome.Valor,
            ValorAtual = indicador.ValorAtual.Valor,
            ValorMaximo = indicador.ValorMaximo.Valor
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var indicador = await _repo.GetByIdAsync(new IndicadorId(id));
        if (indicador == null) return false;

        _repo.Remove(indicador);
        await _unitOfWork.CommitAsync();
        return true;
    }
}
