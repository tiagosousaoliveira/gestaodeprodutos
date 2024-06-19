using GestaoDeProdutos;
using Infra;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Testes.Configuracao
{
    public sealed class IntegrationWebFactory : WebApplicationFactory<Startup>
    {
        public IConfiguration GetConfiguration() => Services.GetRequiredService<IConfiguration>();
        public IServiceScope CreateScope() => Services.CreateScope();
        public static DatabaseContext CreateContext(IServiceScope scope)
            => (DatabaseContext)scope.ServiceProvider.GetService(typeof(DatabaseContext))!;

        public static DatabaseContext CreateContextInMemory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
