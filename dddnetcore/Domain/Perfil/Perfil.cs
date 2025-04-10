using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Perfis
{
    public class Perfil : Entity<PerfilId>, IAggregateRoot
    {
        public DescricaoPerfil Descricao { get; private set; }
        public PMsTotais PMsTotais { get; private set; }

        private readonly List<TipoVinculo> _tiposVinculo = new();
        public IReadOnlyCollection<TipoVinculo> TiposVinculo => _tiposVinculo.AsReadOnly();

        private Perfil() {}

        public Perfil(string descricao, int pmsTotais)
        {
            Id = new PerfilId(Guid.NewGuid());
            Descricao = new DescricaoPerfil(descricao);
            PMsTotais = new PMsTotais(pmsTotais);
        }

        public void AdicionarTipoVinculo(TipoVinculo tipo)
        {
            if (!_tiposVinculo.Contains(tipo))
                _tiposVinculo.Add(tipo);
        }
    }
}