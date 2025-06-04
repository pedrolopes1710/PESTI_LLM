using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Contratos
{
    public class ContratoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContratoRepository _repo;

        public ContratoService(IUnitOfWork unitOfWork, IContratoRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<ContratoDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<ContratoDto> listDto = list.ConvertAll<ContratoDto>(c => new ContratoDto
            {
                Id = c.Id.AsString(),
                Tipo = c.Tipo.ToString(),
                Salario = c.Salario.Valor,
                DataInicio = c.DataInicio.InicioContrato,
                DataFim = c.DataFim?.FimContrato,
                Ativo = c.Ativo
            });

            return listDto;
        }

        public async Task<ContratoDto> GetByIdAsync(ContratoId id)
        {
            var c = await this._repo.GetByIdAsync(id);

            if (c == null)
                return null;

            return new ContratoDto
            {
                Id = c.Id.AsString(),
                Tipo = c.Tipo.ToString(),
                Salario = c.Salario.Valor,
                DataInicio = c.DataInicio.InicioContrato,
                DataFim = c.DataFim?.FimContrato,
                Ativo = c.Ativo
            };
        }

        public async Task<ContratoDto> AddAsync(CreatingContratoDto dto)
        {
            var contrato = new Contrato(
                Enum.Parse<TipoContrato>(dto.Tipo),
                new SalarioMensalContrato(dto.Salario),
                new DataInicioContrato(dto.DataInicio),
                new DataFimContrato(dto.DataFim)
            );

            await this._repo.AddAsync(contrato);
            await this._unitOfWork.CommitAsync();

            return new ContratoDto
            {
                Id = contrato.Id.AsString(),
                Tipo = contrato.Tipo.ToString(),
                Salario = contrato.Salario.Valor,
                DataInicio = contrato.DataInicio.InicioContrato,
                DataFim = contrato.DataFim?.FimContrato,
                Ativo = contrato.Ativo
            };
        }

        public async Task<ContratoDto> UpdateAsync(EditingContratoDto dto)
        {
            var contrato = await this._repo.GetByIdAsync(new ContratoId(dto.Id));

            if (contrato == null)
                return null;

            contrato.AlterarTipo(Enum.Parse<TipoContrato>(dto.Tipo));
            contrato.AlterarSalario(new SalarioMensalContrato(dto.Salario));
            contrato.AlterarDataInicio(new DataInicioContrato(dto.DataInicio));
            contrato.AlterarDataFim(new DataFimContrato(dto.DataFim));

            await this._unitOfWork.CommitAsync();

            return new ContratoDto
            {
                Id = contrato.Id.AsString(),
                Tipo = contrato.Tipo.ToString(),
                Salario = contrato.Salario.Valor,
                DataInicio = contrato.DataInicio.InicioContrato,
                DataFim = contrato.DataFim?.FimContrato,
                Ativo = contrato.Ativo
            };
        }

        public async Task<ContratoDto> InactivateAsync(ContratoId id)
        {
            var contrato = await this._repo.GetByIdAsync(id);

            if (contrato == null)
                return null;

            contrato.Desativar();
            await this._unitOfWork.CommitAsync();

            return new ContratoDto
            {
                Id = contrato.Id.AsString(),
                Tipo = contrato.Tipo.ToString(),
                Salario = contrato.Salario.Valor,
                DataInicio = contrato.DataInicio.InicioContrato,
                DataFim = contrato.DataFim?.FimContrato,
                Ativo = contrato.Ativo
            };
        }


public async Task<ContratoDto> ActivateAsync(ContratoId id)
{
    var contrato = await this._repo.GetByIdAsync(id);

    if (contrato == null)
        return null;

    contrato.Ativar();
    await this._unitOfWork.CommitAsync();

    return new ContratoDto
    {
        Id = contrato.Id.AsString(),
        Tipo = contrato.Tipo.ToString(),
        Salario = contrato.Salario.Valor,
        DataInicio = contrato.DataInicio.InicioContrato,
        DataFim = contrato.DataFim?.FimContrato,
        Ativo = contrato.Ativo
    };
}


        public async Task<ContratoDto> DeleteAsync(ContratoId id)
        {
            var contrato = await this._repo.GetByIdAsync(id);

            if (contrato == null)
                return null;


            this._repo.Remove(contrato);
            await this._unitOfWork.CommitAsync();

            return new ContratoDto
            {
                Id = contrato.Id.AsString(),
                Tipo = contrato.Tipo.ToString(),
                Salario = contrato.Salario.Valor,
                DataInicio = contrato.DataInicio.InicioContrato,
                DataFim = contrato.DataFim?.FimContrato,
                Ativo = contrato.Ativo
            };
        }
    }
}
