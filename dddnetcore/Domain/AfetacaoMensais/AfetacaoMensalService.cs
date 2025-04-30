using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoMensais
{
    public class AfetacaoMensalService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IAfetacaoMensalRepository _repo;

        public AfetacaoMensalService(IUnitOfWork unitOfWork, IAfetacaoMensalRepository repo) {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<AfetacaoMensalDto>> GetAllAsync() {
            return (await this._repo.GetAllAsync()).ConvertAll(af => new AfetacaoMensalDto(af));
        }

        //TODO acabar de implementar o resto dos metodos
    }
}