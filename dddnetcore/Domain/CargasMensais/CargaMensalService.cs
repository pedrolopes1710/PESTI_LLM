using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using dddnetcore.Domain.Pessoas;


namespace dddnetcore.Domain.CargasMensais
{
    public class CargaMensalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICargaMensalRepository _repo;
        private readonly IPessoaRepository _pessoaRepo;


        public CargaMensalService(IUnitOfWork unitOfWork, ICargaMensalRepository repo, IPessoaRepository pessoaRepo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._pessoaRepo = pessoaRepo;
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
                TaxaSocialUnica = c.TSU.Valor,
                PessoaId = c.PessoaId.AsString()
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
            var pessoa = await _pessoaRepo.GetByIdAsync(new PessoaId(Guid.Parse(dto.PessoaId)))
            ?? throw new NullReferenceException("Pessoa não encontrada.");

            //TODO: não deixar criar se já existir outra
            
            var c = new CargaMensal(
                new JornadaDiaria(dto.JornadaDiaria),
                new DiasUteisTrabalhaveis(dto.DiasUteisTrabalhaveis),
                new FeriasBaixasLicencasFaltas(dto.FeriasBaixasLicencasFaltas),
                new SalarioBase(dto.SalarioBase),
                new MesAno(dto.MesAno),
                new TaxaSocialUnica(dto.TaxaSocialUnica)
            );

            pessoa.AdicionarCargaMensal(c);
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
                TaxaSocialUnica = c.TSU.Valor,
                PessoaId = c.PessoaId.AsString()
            };
        }

        public async Task<List<CargaMensalDto>> AddBulkAsync(CreatingBulkCargaMensalDto dto)
        {
            if (dto.MesAnoInicio > dto.MesAnoFim)
                throw new BusinessRuleValidationException("Start date cannot be after end date.");

            List<CargaMensalDto> cargasMensais = [];

            for (var date = dto.MesAnoInicio; date <= dto.MesAnoFim; date = date.AddMonths(1))
            {
                CreatingCargaMensalDto cargaMensalDto = new CreatingCargaMensalDto
                {
                    JornadaDiaria = dto.JornadaDiaria,
                    DiasUteisTrabalhaveis = dto.DiasUteisTrabalhaveis,
                    FeriasBaixasLicencasFaltas = dto.FeriasBaixasLicencasFaltas,
                    MesAno = date,
                    SalarioBase = dto.SalarioBase,
                    TaxaSocialUnica = dto.TaxaSocialUnica,
                    PessoaId = dto.PessoaId
                };
                cargasMensais.Add(await AddAsync(cargaMensalDto));
            }
            return cargasMensais;
        }

        public async Task<CargaMensalDto> UpdateAsync(CargaMensalId Id, EditingCargaMensalDto dto)
        {

            CargaMensal cargaMensal = await this._repo.GetByIdAsync(Id) ?? throw new NullReferenceException("Carga mensal não encontrada: " + Id);

            if (dto.JornadaDiaria != null)
                cargaMensal.AlterarJornada(new JornadaDiaria((double) dto.JornadaDiaria));
            if (dto.DiasUteisTrabalhaveis != null)
                cargaMensal.AlterarDiasUteis(new DiasUteisTrabalhaveis((double) dto.DiasUteisTrabalhaveis));
            if (dto.FeriasBaixasLicencasFaltas != null)
                cargaMensal.AlterarAusencias(new FeriasBaixasLicencasFaltas((double) dto.FeriasBaixasLicencasFaltas));
            if (dto.SalarioBase != null)
                cargaMensal.AlterarSalarioBase(new SalarioBase((double) dto.SalarioBase));
            if (dto.TaxaSocialUnica != null)
                cargaMensal.AlterarTSU(new TaxaSocialUnica((double) dto.TaxaSocialUnica));

            await this._repo.UpdateAsync(cargaMensal);
            await this._unitOfWork.CommitAsync();

            return new CargaMensalDto(cargaMensal);
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
                TaxaSocialUnica = cargaMensal.TSU.Valor,
                PessoaId = cargaMensal.PessoaId.AsString()
            };
        }
    }
}
