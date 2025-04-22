using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Indicadores
{
    public interface IIndicadorRepository : IRepository<Indicador, IndicadorId>
    {
    }
}