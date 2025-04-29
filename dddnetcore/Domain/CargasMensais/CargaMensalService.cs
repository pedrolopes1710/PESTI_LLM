using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.CargasMensais
{
    public class CargaMensalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICargaMensalRepository _repo;

        public CargaMensalService(IUnitOfWork unitOfWork, ICargaMensalRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<CargaMensalDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<CargaMensalDto> listDto = list.ConvertAll<CargaMensalDto>(c => new CargaMensalDto
            {
            Id = c.Id.AsString(),
            JornadaDiaria = c.JornadaDiaria.Valor,        //
            DiasUteisTrabalhaveis = c.DiasUteis.Valor,
            FeriasBaixasLicencasFaltas = c.Ausencias.Dias,
            MesAno = c.MesAno.Valor,
            SalarioBase = c.SalarioBase.Valor,
            TaxaSocialUnica = c.TSU.Valor
            });

            return listDto;
        }

        public async Task<CargaMensalDto> GetByIdAsync(CargaMensalId id)
        {
            var c = await this._repo.GetByIdAsync(id);

            if (c == null)
                return null;

            return new CargaMensalDto(c);
        }

        public async Task<CargaMensalDto> AddAsync(CreatingCargaMensalDto dto)
        {
            var c = new CargaMensal(
                new JornadaDiaria(dto.JornadaDiaria),
                new DiasUteisTrabalhaveis(dto.DiasUteisTrabalhaveis),
                new FeriasBaixasLicencasFaltas(dto.FeriasBaixasLicencasFaltas),
                new SalarioBase(dto.SalarioBase),
                new MesAno(dto.MesAno),
                new TaxaSocialUnica(dto.TaxaSocialUnica)
            );

            await this._repo.AddAsync(c);
            await this._unitOfWork.CommitAsync();

            return new CargaMensalDto
            {
                Id = c.Id.AsString(),
                JornadaDiaria = c.JornadaDiaria.Valor,        //
                DiasUteisTrabalhaveis = c.DiasUteis.Valor,
                FeriasBaixasLicencasFaltas = c.Ausencias.Dias,
                MesAno = c.MesAno.Valor,
                SalarioBase = c.SalarioBase.Valor,
                TaxaSocialUnica = c.TSU.Valor
            };
        }

        public async Task<CargaMensalDto> UpdateAsync(EditingCargaMensalDto dto)
        {
            var cargaMensal = await this._repo.GetByIdAsync(new CargaMensalId(dto.Id));

            if (cargaMensal == null)
                return null;

            cargaMensal.AlterarJornada(new JornadaDiaria(dto.JornadaDiaria));
            cargaMensal.AlterarDiasUteis(new DiasUteisTrabalhaveis(dto.DiasUteisTrabalhaveis));
            cargaMensal.AlterarAusencias(new FeriasBaixasLicencasFaltas(dto.FeriasBaixasLicencasFaltas));
            cargaMensal.AlterarSalarioBase(new SalarioBase(dto.SalarioBase));
            cargaMensal.AlterarTSU(new TaxaSocialUnica(dto.TaxaSocialUnica));

            await this._unitOfWork.CommitAsync();

            return new CargaMensalDto
            {
                Id = cargaMensal.Id.AsString(),
                JornadaDiaria = cargaMensal.JornadaDiaria.Valor,        //
                DiasUteisTrabalhaveis = cargaMensal.DiasUteis.Valor,
                FeriasBaixasLicencasFaltas = cargaMensal.Ausencias.Dias,
                MesAno = cargaMensal.MesAno.Valor,
                SalarioBase = cargaMensal.SalarioBase.Valor,
                TaxaSocialUnica = cargaMensal.TSU.Valor
            };
        }

        public async Task<CargaMensalDto> DeleteAsync(CargaMensalId id)
        {
            var cargaMensal = await this._repo.GetByIdAsync(id);

            if (cargaMensal == null)
                return null;


            this._repo.Remove(cargaMensal);
            await this._unitOfWork.CommitAsync();

            return new CargaMensalDto
            {
                Id = cargaMensal.Id.AsString(),
                JornadaDiaria = cargaMensal.JornadaDiaria.Valor,        //
                DiasUteisTrabalhaveis = cargaMensal.DiasUteis.Valor,
                FeriasBaixasLicencasFaltas = cargaMensal.Ausencias.Dias,
                MesAno = cargaMensal.MesAno.Valor,
                SalarioBase = cargaMensal.SalarioBase.Valor,
                TaxaSocialUnica = cargaMensal.TSU.Valor
            };
        }
    }
}
