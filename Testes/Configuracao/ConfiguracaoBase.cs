using Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Testes.Configuracao
{
    public class ConfiguracaoBase
    {
        private readonly IntegrationWebFactory _factory;
        private readonly HttpClient _httpClient;
        private readonly DatabaseContext databaseContext;
        private IServiceScope _scope { get; }
        public ConfiguracaoBase(IntegrationWebFactory factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
            _scope = _factory.CreateScope();
            databaseContext = IntegrationWebFactory.CreateContext(_scope);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public void Dispose()
        {
            _httpClient.Dispose();
            RemoveEntidades();
            databaseContext.Dispose();
        }

        public DatabaseContext ObtemContext() => databaseContext;
        public DatabaseContext CreateContextInmemory() => IntegrationWebFactory.CreateContextInMemory();

        public async Task<HttpResponseMessage> DoRequest(Func<HttpClient, Task<HttpResponseMessage>> execution)
        {
            var response = await execution.Invoke(_httpClient);
            return response;
        }

        public void RemoveEntidades()
        {
            databaseContext.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Produtos\"");
            databaseContext.Database.ExecuteSqlRaw("ALTER SEQUENCE public.\"Produtos_CodigoProduto_seq\" RESTART WITH 1;");
            databaseContext.SaveChanges();

        }
    }
}