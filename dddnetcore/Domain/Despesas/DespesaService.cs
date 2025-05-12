using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.AfetacaoMensais;
using dddnetcore.Domain.AfetacaoPerfis;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Pessoas;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Despesas
{
    public class DespesaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDespesaRepository _repo;
        private readonly ICargaMensalRepository _cargaMensalRepo;
        private readonly IPessoaRepository _pessoaRepo;
        private readonly IAfetacaoMensalRepository _afetacaoMensalRepo;
        private readonly IAtividadeRepository _atividadeRepo;
        private readonly OrcamentoService _orcamentoService;
        private readonly IRubricaRepository _rubricaRepo;
        private readonly RubricaService _rubricaService;
        private readonly IOrcamentoRepository _orcamentoRepo;

        public DespesaService(IUnitOfWork unitOfWork, IDespesaRepository repo, ICargaMensalRepository cargaMensalRepository, IPessoaRepository pessoaRepository, IAfetacaoMensalRepository afetacaoMensalRepository, IAtividadeRepository atividadeRepositor, OrcamentoService orcamentoService, IRubricaRepository rubricaRepository, RubricaService rubricaService, IOrcamentoRepository orcamentoRepository) {   
            if (unitOfWork == null) throw new ArgumentNullException("Unit of Work cannot be null!");
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._cargaMensalRepo = cargaMensalRepository;
            this._pessoaRepo = pessoaRepository;
            this._afetacaoMensalRepo = afetacaoMensalRepository;
            this._atividadeRepo = atividadeRepositor;
            this._orcamentoService = orcamentoService;
            this._rubricaRepo = rubricaRepository;
            this._rubricaService = rubricaService;
            this._orcamentoRepo = orcamentoRepository;
        }

        public async Task<List<DespesaDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(despesa => new DespesaDto(despesa));
        }

        public async Task<DespesaDto> GetByIdAsync(DespesaId id) {
            Despesa despesa = await this._repo.GetByIdAsync(id);

            return despesa == null ? null : new DespesaDto(despesa);
        }

        public async Task<DespesaDto> AddAsync(CreatingDespesaDto dto) {
            if (dto.Descricao == null) 
                throw new ArgumentNullException("Missing name for Expense creation!");

            Despesa despesa = new Despesa(new DescricaoDespesa(dto.Descricao), new ValorDespesa((double)dto.Valor), null, new Automatico(false), new OrcamentoId(dto.OrcamentoId));
        
            await this._repo.AddAsync(despesa);
            await this._unitOfWork.CommitAsync();

            return new DespesaDto(despesa);
        }

        public async Task<List<DespesaDto>> GenerateAsync(GeneratingDespesaDto dto) {
            CargaMensalId cargaMensalId = dto.cargaMensalId;
            CargaMensal cargaMensal = await this._cargaMensalRepo.GetByIdAsync(cargaMensalId) ?? throw new NullReferenceException("Not Found Montly Load: " + cargaMensalId);
            Pessoa pessoa = await this._pessoaRepo.GetByIdAsync(cargaMensal.PessoaId) ?? throw new NullReferenceException("Not Found Person: " + cargaMensal.PessoaId);
            List<Despesa> novasDespesas = [];
            List<AfetacaoMensal> afetacoesMensais = await this._afetacaoMensalRepo.GetByCargaMensalIdAsync(cargaMensalId) ?? throw new NullReferenceException("Not Found Monthly Load: " + cargaMensalId);

            foreach (AfetacaoMensal afetacaoMensal in afetacoesMensais) {
                //* se n der deve ser por falta de .includes
                AtividadeId atividadeId = afetacaoMensal.AfetacaoPerfil.Perfil.AtividadeId;
                Atividade atividade = await this._atividadeRepo.GetByIdAsync(atividadeId);
                if (atividade == null) {
                    Console.WriteLine("Atividade não encontrada: " + atividadeId);
                    continue;
                }
                Orcamento orcamento = GetSalarial(atividade.Orcamentos);
                if (orcamento == null) {
                    Rubrica rubricaSalarial = await this._rubricaRepo.GetSalarial();
                    Guid rubricaId;
                    if (rubricaSalarial == null) {
                        rubricaId = (await _rubricaService.AddAsync(new CreatingRubricaDto{Nome=NomeRubrica.NomeSalarial})).Id;
                    } else {
                        rubricaId = rubricaSalarial.Id.AsGuid();
                    }
                    orcamento = new Orcamento(new GastoPlaneado(0), rubricaSalarial);
                    await this._orcamentoService.AddAsync(new CreatingOrcamentoDto{GastoPlaneado=0, RubricaId=rubricaId});
                    
                    atividade.AddOrcamento(orcamento);
                    await _atividadeRepo.UpdateAsync(atividade);
                    await _unitOfWork.CommitAsync();
                }
                double valor = afetacaoMensal.PMs.Quantidade * cargaMensal.SalarioBase.Valor * (1 + cargaMensal.TSU.Valor);
                string descricao = $"Salary of month {cargaMensal.MesAno.Valor.ToString("MM/yyyy")} for {pessoa.Nome}.";
            
                
                Despesa despesa = new Despesa(new DescricaoDespesa(descricao), new ValorDespesa(valor), cargaMensal, new Automatico(true), orcamento.Id);
                novasDespesas.Add(despesa);
                await this._repo.AddAsync(despesa);
                await _unitOfWork.CommitAsync();

                orcamento.AddDespesa(despesa);
                await _orcamentoRepo.UpdateAsync(orcamento);
                await _unitOfWork.CommitAsync();
            }

            return novasDespesas.ConvertAll(despesa => new DespesaDto(despesa));
        }

        public async Task<DespesaDto> EditAsync(DespesaId id, EditingDespesaDto dto) {
            Despesa despesa = await this._repo.GetByIdAsync(id) ?? throw new NullReferenceException("Not Found Expense: " + id);
            if (dto.Descricao != null) {
                despesa.EditDescricao(new DescricaoDespesa(dto.Descricao));
            }
            if (dto.Valor != null) {
                despesa.EditValor(new ValorDespesa((double)dto.Valor));
            }
            await this._repo.UpdateAsync(despesa);
            await this._unitOfWork.CommitAsync();

            return new DespesaDto(despesa);
        }

        public async Task<DespesaDto> DeleteAsync(DespesaId id) {
            Despesa despesa = await this._repo.GetByIdAsync(id) ?? throw new NullReferenceException("Not Found Expense: " + id);

            if (despesa.Automatico.Auto) {
                throw new BusinessRuleValidationException("Cannot delete an automatic expense!");
            }
            
            this._repo.Remove(despesa);
            await this._unitOfWork.CommitAsync();

            return new DespesaDto(despesa);
        }

        private static Orcamento? GetSalarial(List<Orcamento> orcamentos) {
            return orcamentos.FirstOrDefault(o => o.Rubrica.Nome.Nome.Equals(NomeRubrica.NomeSalarial)) ?? null;
        }
    }
}