using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.Projetos;
using dddnetcore.Domain.CargasMensais;
using DDDSample1.Domain.Shared;
using FluentAssertions;

namespace dddnetcore.Domain.Pessoas
{
    public class PessoaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPessoaRepository _repo;
        private readonly IContratoRepository _contratoRepo;
        private readonly IProjetoRepository _projetoRepo;
        private readonly ICargaMensalRepository _cargaMensalRepo;


        public PessoaService(IUnitOfWork unitOfWork, IPessoaRepository repo, IContratoRepository contratoRepo, IProjetoRepository projetoRepo, ICargaMensalRepository cargaMensalRepo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
            _contratoRepo = contratoRepo;
            _projetoRepo = projetoRepo;
            _cargaMensalRepo = cargaMensalRepo;
        }

        public async Task<PessoaDto> GetByIdAsync(PessoaId id)
        {
            var pessoa = await _repo.GetByIdAsync(id)
                ?? throw new NullReferenceException($"Pessoa não encontrada com o ID: {id}");


            // Buscar os dados relacionados
            var cargas = await _cargaMensalRepo.GetByPessoaIdAsync(id); // método no CargaMensalRepository
            var contrato = await _contratoRepo.GetByPessoaIdAsync(id); // método no ContratoRepository

            // Retornar o DTO completo
            return new PessoaDto
            {
                Id = pessoa.Id.AsGuid(),
                Nome = pessoa.Nome.Value,
                Email = pessoa.Email.Value,
                PessoaCienciaId = pessoa.CienciaId.Value,
                PessoaUltimoPedPagam = pessoa.UltimoPedidoPagamento.Value,
                Ativo = pessoa.Ativo,
                Contrato = contrato != null ? new ContratoDto
                {
                    Id = contrato.Id.AsString(),
                    DataInicio = contrato.DataInicio.InicioContrato,
                    DataFim = contrato.DataFim.FimContrato,
                    Salario = contrato.Salario.Valor,
                    Tipo = contrato.Tipo.ToString()
                } : null,
                CargasMensais = cargas.Select(c => new CargaMensalDto
                {
                    Id = c.Id.AsString(),
                    JornadaDiaria = c.JornadaDiaria.Valor,
                    DiasUteisTrabalhaveis = c.DiasUteis.Valor,
                    FeriasBaixasLicencasFaltas = c.Ausencias.Dias,
                    MesAno = c.MesAno.Valor,
                    SalarioBase = c.SalarioBase.Valor,
                    TaxaSocialUnica = c.TSU.Valor
                }).ToList(),
                Projetos = pessoa.Projetos.Select(p => new ProjetoDTO
                {
                    Id = p.Id.AsGuid(),
                    Nome = p.NomeProjeto.Valor,
                    Descricao = p.DescricaoProjeto.Valor
                }).ToList()
            };
        }

        public async Task<List<PessoaDto>> GetAllAsync()
        {
            var pessoas = await _repo.GetAllAsync();

            var todasCargas = await _cargaMensalRepo.GetAllAsync();
            //var todosProjetos = await _projetoRepo.GetAllAsync();
            var todosContratos = await _contratoRepo.GetAllAsync();

            var dtos = pessoas.Select(p => new PessoaDto
            {
                Id = p.Id.AsGuid(),
                Nome = p.Nome.Value,
                Email = p.Email.Value,
                PessoaCienciaId = p.CienciaId.Value,
                PessoaUltimoPedPagam = p.UltimoPedidoPagamento.Value,
                Ativo = p.Ativo,
                Contrato = todosContratos
                    .Where(contrato => contrato.Id == p.ContratoId)
                    .Select(contrato => new ContratoDto
                    {
                        Id = contrato.Id.AsString(),
                        Tipo = contrato.Tipo.ToString(),
                        Salario = contrato.Salario.Valor,
                        DataInicio = contrato.DataInicio.InicioContrato,
                        DataFim = contrato.DataFim.FimContrato,
                        Ativo = contrato.Ativo
                    })
                    .FirstOrDefault(),
                CargasMensais = todasCargas
                    .Where(c => c.PessoaId == p.Id)
                    .Select(c => new CargaMensalDto
                    {
                        Id = c.Id.AsString(),
                        JornadaDiaria = c.JornadaDiaria.Valor,
                        DiasUteisTrabalhaveis = c.DiasUteis.Valor,
                        FeriasBaixasLicencasFaltas = c.Ausencias.Dias,
                        MesAno = c.MesAno.Valor,
                        SalarioBase = c.SalarioBase.Valor,
                        TaxaSocialUnica = c.TSU.Valor
                    })
                    .ToList(),
                Projetos = p.Projetos?.Select(proj => new ProjetoDTO
                {
                    Id = proj.Id.AsGuid(),
                    Nome = proj.NomeProjeto.Valor,

                }).ToList()
            }).ToList();

            return dtos;
        }


        public async Task<PessoaDto> AddAsync(CreatingPessoaDto dto)
        {
            Contrato contrato = null;

            if (!string.IsNullOrEmpty(dto.ContratoId))
            {
                contrato = await _contratoRepo.GetByIdAsync(new ContratoId(dto.ContratoId));
                if (contrato == null) throw new NullReferenceException("Contrato não encontrado");

                var pessoaComMesmoContrato = await _repo.GetByContratoIdAsync(new ContratoId(dto.ContratoId));
                if (pessoaComMesmoContrato != null)
                    throw new InvalidOperationException("Este contrato já está associado a outra pessoa.");
            }

            var pessoa = new Pessoa(
            new NomePessoa(dto.Nome),
            new EmailPessoa(dto.Email),
            new PessoaCienciaId(dto.PessoaCienciaId),
            new PessoaUltimoPedPagam(dto.PessoaUltimoPedPagam),
            contrato
            );


            await _repo.AddAsync(pessoa);
            await _unitOfWork.CommitAsync();

            return new PessoaDto
            {
                Id = pessoa.Id.AsGuid(),
                Nome = pessoa.Nome.Value,
                Email = pessoa.Email.Value,
                PessoaCienciaId = pessoa.CienciaId.Value,
                PessoaUltimoPedPagam = pessoa.UltimoPedidoPagamento.Value,
                Ativo = pessoa.Ativo,
                Contrato = contrato == null ? null : new ContratoDto
                {
                    Id = contrato.Id.AsString(),
                    DataInicio = contrato.DataInicio.InicioContrato,
                    DataFim = contrato.DataFim.FimContrato,
                    Salario = contrato.Salario.Valor,
                    Tipo = contrato.Tipo.ToString()
                }
            };
        }

        public async Task<PessoaDto> UpdateAsync(EditingPessoaDto dto)
        {

            Contrato contrato = null;
            var pessoa = await _repo.GetByIdAsync(new PessoaId(dto.Id));
            if (pessoa == null) throw new BusinessRuleValidationException("Pessoa não encontrada");

            pessoa.AlterarNome(new NomePessoa(dto.Nome));
            pessoa.AlterarEmail(new EmailPessoa(dto.Email));
            pessoa.AlterarUltimoPedidoPagamento(new PessoaUltimoPedPagam(dto.PessoaUltimoPedPagam));


            await _unitOfWork.CommitAsync();

            return new PessoaDto
            {
                Id = pessoa.Id.AsGuid(),
                Nome = pessoa.Nome.Value,
                Email = pessoa.Email.Value,
                PessoaCienciaId = pessoa.CienciaId.Value,
                PessoaUltimoPedPagam = pessoa.UltimoPedidoPagamento.Value,
                Ativo = pessoa.Ativo,
                Contrato = contrato == null ? null : new ContratoDto
                {
                    Id = contrato.Id.AsString(),
                    DataInicio = contrato.DataInicio.InicioContrato,
                    DataFim = contrato.DataFim.FimContrato,
                    Salario = contrato.Salario.Valor,
                    Tipo = contrato.Tipo.ToString()
                }
            };
        }


        public async Task<PessoaDto> DeleteAsync(PessoaId id)
        {
            var pessoa = await _repo.GetByIdAsync(id)
                ?? throw new BusinessRuleValidationException("Pessoa não encontrada");

            _repo.Remove(pessoa);
            await _unitOfWork.CommitAsync();

            return new PessoaDto(pessoa);
        }


        //Associar Projetos
        public async Task<PessoaDto> AssociarProjetosAsync(AssociarProjetoDto dto)
        {
            var pessoa = await _repo.GetByIdAsync(new PessoaId(Guid.Parse(dto.PessoaId)));
            if (pessoa == null) throw new NullReferenceException("Pessoa não encontrada");

            var projetos = await _projetoRepo.GetByIdsAsync(dto.ProjetosIds.Select(id => new ProjetoId(Guid.Parse(id))));

            foreach (var projeto in projetos)
            {
                pessoa.Projetos.Add(projeto);
            }

            await _unitOfWork.CommitAsync();

            // Recolher dados relacionados via os novos métodos
            var cargas = await _cargaMensalRepo.GetByPessoaIdAsync(pessoa.Id);
            var contrato = await _contratoRepo.GetByPessoaIdAsync(pessoa.Id);

            return new PessoaDto
            {
                Id = pessoa.Id.AsGuid(),
                Nome = pessoa.Nome.Value,
                Email = pessoa.Email.Value,
                PessoaCienciaId = pessoa.CienciaId.Value,
                PessoaUltimoPedPagam = pessoa.UltimoPedidoPagamento.Value,
                Ativo = pessoa.Ativo,
                Contrato = contrato == null ? null : new ContratoDto
                {
                    Id = contrato.Id.AsString(),
                    DataInicio = contrato.DataInicio.InicioContrato,
                    DataFim = contrato.DataFim.FimContrato,
                    Salario = contrato.Salario.Valor,
                    Tipo = contrato.Tipo.ToString()
                },
                CargasMensais = cargas.Select(c => new CargaMensalDto
                {
                    Id = c.Id.AsString(),
                    JornadaDiaria = c.JornadaDiaria.Valor,
                    DiasUteisTrabalhaveis = c.DiasUteis.Valor,
                    FeriasBaixasLicencasFaltas = c.Ausencias.Dias,
                    MesAno = c.MesAno.Valor,
                    SalarioBase = c.SalarioBase.Valor,
                    TaxaSocialUnica = c.TSU.Valor
                }).ToList(),
                Projetos = pessoa.Projetos.Select(p => new ProjetoDTO
                {
                    Id = p.Id.AsGuid(),
                    Nome = p.NomeProjeto.Valor,
                    Descricao = p.DescricaoProjeto.Valor
                }).ToList()
            };
        }


        public async Task<PessoaDto> DesassociarProjetosAsync(AssociarProjetoDto dto)
        {
            var pessoa = await _repo.GetByIdAsync(new PessoaId(Guid.Parse(dto.PessoaId)));
            if (pessoa == null) throw new NullReferenceException("Pessoa não encontrada");

            var contrato = await _contratoRepo.GetByPessoaIdAsync(pessoa.Id);

            var cargas = await _cargaMensalRepo.GetByPessoaIdAsync(pessoa.Id);


            var projetosARemover = pessoa.Projetos
                .Where(p => dto.ProjetosIds.Contains(p.Id.AsString()))
                .ToList();

            foreach (var projeto in projetosARemover)
            {
                pessoa.Projetos.Remove(projeto);
            }

            await _unitOfWork.CommitAsync();

            return new PessoaDto
            {
                Id = pessoa.Id.AsGuid(),
                Nome = pessoa.Nome.Value,
                Email = pessoa.Email.Value,
                PessoaCienciaId = pessoa.CienciaId.Value,
                PessoaUltimoPedPagam = pessoa.UltimoPedidoPagamento.Value,
                Ativo = pessoa.Ativo,
                Contrato = contrato == null ? null : new ContratoDto
                {
                    Id = contrato.Id.AsString(),
                    DataInicio = contrato.DataInicio.InicioContrato,
                    DataFim = contrato.DataFim.FimContrato,
                    Salario = contrato.Salario.Valor,
                    Tipo = contrato.Tipo.ToString()
                },
                CargasMensais = cargas.Select(c => new CargaMensalDto
                {
                    Id = c.Id.AsString(),
                    JornadaDiaria = c.JornadaDiaria.Valor,
                    DiasUteisTrabalhaveis = c.DiasUteis.Valor,
                    FeriasBaixasLicencasFaltas = c.Ausencias.Dias,
                    MesAno = c.MesAno.Valor,
                    SalarioBase = c.SalarioBase.Valor,
                    TaxaSocialUnica = c.TSU.Valor
                }).ToList(),
                Projetos = pessoa.Projetos.Select(p => new ProjetoDTO
                {
                    Id = p.Id.AsGuid(),
                    Nome = p.NomeProjeto.Valor,
                    Descricao = p.DescricaoProjeto.Valor
                }).ToList()
            };
        }

        public async Task<PessoaDto> RemoverContratoAsync(PessoaId pessoaId)
        {
            var pessoa = await _repo.GetByIdAsync(pessoaId)
                ?? throw new NullReferenceException("Pessoa não encontrada");

            pessoa.RemoverContrato(); // método na entidade que seta Contrato e ContratoId a null

            await _unitOfWork.CommitAsync();

            return new PessoaDto(pessoa);
        }

        public async Task<PessoaDto> AssociarContratoAsync(string pessoaId, string contratoId)
        {
            var pessoa = await _repo.GetByIdAsync(new PessoaId(Guid.Parse(pessoaId)))
                ?? throw new NullReferenceException("Pessoa não encontrada");

            var contrato = await _contratoRepo.GetByIdAsync(new ContratoId(contratoId))
                ?? throw new NullReferenceException("Contrato não encontrado");

            // Validar se o contrato já está em uso
            var pessoaComMesmoContrato = await _repo.GetByContratoIdAsync(contrato.Id);
            if (pessoaComMesmoContrato != null && pessoaComMesmoContrato.Id != pessoa.Id)
                throw new InvalidOperationException("Este contrato já está associado a outra pessoa.");

            pessoa.AssociarContrato(contrato); // novo método

            await _unitOfWork.CommitAsync();

            return new PessoaDto
            {
                Id = pessoa.Id.AsGuid(),
                Nome = pessoa.Nome.Value,
                Email = pessoa.Email.Value,
                PessoaCienciaId = pessoa.CienciaId.Value,
                PessoaUltimoPedPagam = pessoa.UltimoPedidoPagamento.Value,
                Ativo = pessoa.Ativo,
                Contrato = contrato == null ? null : new ContratoDto
                {
                    Id = contrato.Id.AsString(),
                    DataInicio = contrato.DataInicio.InicioContrato,
                    DataFim = contrato.DataFim.FimContrato,
                    Salario = contrato.Salario.Valor,
                    Tipo = contrato.Tipo.ToString()
                },
                Projetos = pessoa.Projetos.Select(p => new ProjetoDTO
                {
                    Id = p.Id.AsGuid(),
                    Nome = p.NomeProjeto.Valor,
                    Descricao = p.DescricaoProjeto.Valor
                }).ToList()
            };
        }


        public async Task<PessoaDto> DesativarPessoaAsync(string pessoaId)
        {
            var pessoa = await _repo.GetByIdAsync(new PessoaId(Guid.Parse(pessoaId)));
            if (pessoa == null)
                throw new BusinessRuleValidationException("Pessoa não encontrada");

            pessoa.Desativar();

            await _unitOfWork.CommitAsync();

            return new PessoaDto
            {
                Id = pessoa.Id.AsGuid(),
                Nome = pessoa.Nome.Value,
                Email = pessoa.Email.Value,
                PessoaCienciaId = pessoa.CienciaId.Value,
                PessoaUltimoPedPagam = pessoa.UltimoPedidoPagamento.Value,
                Ativo = pessoa.Ativo
            };            

        }
        
        public async Task<PessoaDto> ReativarPessoaAsync(string pessoaId)
        {
            var pessoa = await _repo.GetByIdAsync(new PessoaId(Guid.Parse(pessoaId)));
            if (pessoa == null)
                throw new BusinessRuleValidationException("Pessoa não encontrada");

            pessoa.Reativar();

            await _unitOfWork.CommitAsync();

            return new PessoaDto
            {
                Id = pessoa.Id.AsGuid(),
                Nome = pessoa.Nome.Value,
                Email = pessoa.Email.Value,
                PessoaCienciaId = pessoa.CienciaId.Value,
                PessoaUltimoPedPagam = pessoa.UltimoPedidoPagamento.Value,
                Ativo = pessoa.Ativo
            }; 
        }






    }
}
