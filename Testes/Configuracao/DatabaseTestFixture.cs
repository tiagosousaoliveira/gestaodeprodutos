using Xunit;

namespace Testes.Configuracao
{
    public class DatabaseTestFixture : IClassFixture<IntegrationWebFactory>
    {
        protected readonly IntegrationWebFactory _factory;
        public DatabaseTestFixture(IntegrationWebFactory factory)
        {
            _factory = factory;
        }
        public ConfiguracaoBase NewRequest()
            => new ConfiguracaoBase(_factory);

    }
}
