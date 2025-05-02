using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.Projetos;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Pessoas
{
    public class PessoaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPessoaRepository _repo;
        private readonly IContratoRepository _contratoRepo;
        private readonly IProjetoRepository _projetoRepo;

        public PessoaService(IUnitOfWork unitOfWork, IPessoaRepository repo, IContratoRepository contratoRepo, IProjetoRepository projetoRepo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
            _contratoRepo = contratoRepo;
            _projetoRepo = projetoRepo;
        }

        public async Task<PessoaDto> GetByIdAsync(PessoaId id)
        {
            var pessoa = await _repo.GetByIdAsync(id)
                ?? throw new NullReferenceException($"Pessoa não encontrada com o ID: {id}");

            return new PessoaDto(pessoa);
        }

        public async Task<List<PessoaDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            
            List<PessoaDto> listDto = list.ConvertAll<PessoaDto>(c => new PessoaDto(c));

            return listDto;
        }

        public async Task<PessoaDto> AddAsync(CreatingPessoaDto dto)
        {
            Contrato contrato = null;

            if (!string.IsNullOrEmpty(dto.ContratoId))
            {
                contrato = await _contratoRepo.GetByIdAsync(new ContratoId(dto.ContratoId));
                if (contrato == null) throw new NullReferenceException("Contrato não encontrado");
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

            return new PessoaDto(pessoa);
        }

        public async Task<PessoaDto> UpdateAsync(EditingPessoaDto dto)
        {
            var pessoa = await _repo.GetByIdAsync(new PessoaId(dto.Id));
            if (pessoa == null) throw new BusinessRuleValidationException("Pessoa não encontrada");

            pessoa.AlterarNome(new NomePessoa(dto.Nome));
            pessoa.AlterarEmail(new EmailPessoa(dto.Email));
            pessoa.AlterarUltimoPedidoPagamento(new PessoaUltimoPedPagam(dto.PessoaUltimoPedPagam));

            // Atualiza o contrato se for fornecido, senão remove o vínculo
            if (!string.IsNullOrEmpty(dto.ContratoId))
            {
                var contrato = await _contratoRepo.GetByIdAsync(new ContratoId(dto.ContratoId));
                if (contrato == null) throw new NullReferenceException("Contrato não encontrado");

                pessoa.AlterarContrato(contrato);
            }
            else
            {
            // Se o contrato foi removido, define como null
            pessoa.AlterarContrato(null);
            }

            await _unitOfWork.CommitAsync();

            return new PessoaDto(pessoa);
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

            return new PessoaDto(pessoa); // Certifique-se que o MapToDto inclui projetos se necessário
        }


    }
}
