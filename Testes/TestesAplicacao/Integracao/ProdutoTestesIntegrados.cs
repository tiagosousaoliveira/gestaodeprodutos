using Domain.Dto;
using Domain.Model;
using GestaoDeProdutos.Inputs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Testes.Configuracao;
using Xunit;

namespace Testes.TestesAplicacao.Integracao
{
    public class ProdutoTestesIntegrados : DatabaseTestFixture
    {
        private const string Uri = "http://localhost:14601/api/produtos";
        public ProdutoTestesIntegrados(IntegrationWebFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task DeveObterOProduto_ERetornarSucesso()
        {
            var baseConfig = NewRequest();
            baseConfig.RemoveEntidades();

            var produto = new Produto()
            {
                DescricaoProduto = "Produto de Teste",
                SituacaoProduto = true,
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(30),
                CodigoFornecedor = 1,
                DescricaoFornecedor = "Fornecedor de Teste",
                CnpjFornecedor = "12345678901234"
            };
            var context = baseConfig.ObtemContext();
            context.Add(produto);
            context.SaveChanges();

            try
            {
                var response = await baseConfig.DoRequest(async http =>
                {
                    return await http.GetAsync(Uri + $"/{1}");
                });

                var produtoResponse = await response.Content.ReadFromJsonAsync<Produto>();

                Assert.True(response.IsSuccessStatusCode);
                Assert.NotNull(produtoResponse);
                Assert.Equal(produto.DescricaoProduto, produtoResponse.DescricaoProduto);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
            finally
            {
                baseConfig.Dispose();
            }

        }

        [Fact]
        public async Task DeveObterAlistaDosProdutosFiltrado_ERetornarSucesso()
        {
            var baseConfig = NewRequest();
            baseConfig.RemoveEntidades();

            var produtos = ObtemListaDeProdutos();
            var context = baseConfig.ObtemContext();
            context.AddRange(produtos);
            context.SaveChanges();

            var response = await baseConfig.DoRequest(async http =>
            {
                return await http.GetAsync($"{Uri}?DescricaoProduto=Produto%20de%20Teste&SituacaoProduto=true&Pagina=1&QuantidadePorPagina=10");
            });

            var produtosResponse = await response.Content.ReadFromJsonAsync<PaginacaoDto<Produto>>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(produtosResponse);
            Assert.Equal(10, produtosResponse.TotalRecords);
            Assert.Equal(3, produtosResponse.Data.Count());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveObterAlistaDosProdutosSemFiltroEPaginad_ERetornarSucesso()
        {
            var baseConfig = NewRequest();
            baseConfig.RemoveEntidades();

            var produtos = ObtemListaDeProdutos();
            var context = baseConfig.ObtemContext();
            context.AddRange(produtos);
            context.SaveChanges();

            var response = await baseConfig.DoRequest(async http =>
            {
                return await http.GetAsync($"{Uri}");
            });

            var produtosResponse = await response.Content.ReadFromJsonAsync<PaginacaoDto<Produto>>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(produtosResponse);
            Assert.Equal(10, produtosResponse.TotalRecords);
            Assert.Equal(8, produtosResponse.Data.Count());
            Assert.Equal(2, produtosResponse.TotalPages);
            Assert.Equal(1, produtosResponse.PageNumber);
        }

        [Fact]
        public async Task DeveSalvarOProduto_ERetornarSucesso()
        {
            var baseConfig = NewRequest();
            baseConfig.RemoveEntidades();
            var produtoInput = new IncluirProdutoInput(
                "Produto de Teste",
                true,
                DateTime.Now,
                DateTime.Now.AddDays(30),
                1,
                "Fornecedor de Teste",
                "12345678901234"
            );

            try
            {
                var response = await baseConfig.DoRequest(async http =>
                {
                    return await http.PostAsJsonAsync(Uri, produtoInput);
                });

                var context = baseConfig.ObtemContext();
                var produtosDb = context.Produtos.AsNoTracking().ToList();

                Assert.True(response.IsSuccessStatusCode);
                Assert.NotNull(produtosDb);
                Assert.Single(produtosDb);
                Assert.Equal(produtoInput.DescricaoProduto, produtosDb.Select(x => x.DescricaoProduto).First());
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
            finally
            {
                baseConfig.Dispose();
            }
        }

        [Fact]
        public async Task DeveAtualizarOProduto_ERetornarSucesso()
        {
            var baseConfig = NewRequest();
            baseConfig.RemoveEntidades();
            var produto = new Produto()
            {
                DescricaoProduto = "Produto de Teste Anterior",
                SituacaoProduto = true,
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(30),
                CodigoFornecedor = 1,
                DescricaoFornecedor = "Fornecedor de Teste Anterior",
                CnpjFornecedor = "12345678901234"
            };
            var context = baseConfig.ObtemContext();
            context.Add(produto);
            context.SaveChanges();

            var produtoInput = new AtualizarProdutoInput(
                1,
                "Produto de Teste Novo",
                true,
                DateTime.Now,
                DateTime.Now.AddDays(30),
                1,
                "Fornecedor de Teste Novo",
                "12345678901234"
            );

            try
            {
                var response = await baseConfig.DoRequest(async http =>
                {
                    return await http.PutAsJsonAsync(Uri, produtoInput);
                });

                var newContext = baseConfig.ObtemContext();
                var produtosDb = context.Produtos.AsNoTracking().ToList();

                Assert.True(response.IsSuccessStatusCode);
                Assert.NotNull(produtosDb);
                Assert.Single(produtosDb);
                Assert.Equal(new(produtoInput.DescricaoProduto, produtoInput.DescricaoFornecedor),
                        produtosDb.Select(x => (x.DescricaoProduto, x.DescricaoFornecedor)).First());
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
            finally
            {
                baseConfig.Dispose();
            }
        }

        [Fact]
        public async Task DeveCancelarOProduto_ERetornarSucesso()
        {
            var baseConfig = NewRequest();
            baseConfig.RemoveEntidades();
            var produto = new Produto()
            {
                DescricaoProduto = "Produto de Teste",
                SituacaoProduto = true,
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(30),
                CodigoFornecedor = 1,
                DescricaoFornecedor = "Fornecedor de Teste",
                CnpjFornecedor = "12345678901234"
            };
            var context = baseConfig.ObtemContext();
            context.Add(produto);
            context.SaveChanges();

            try
            {
                var response = await baseConfig.DoRequest(async http =>
                {
                    return await http.DeleteAsync($"{Uri}/{1}");
                });

                var newContext = baseConfig.ObtemContext();
                var produtosDb = context.Produtos.AsNoTracking().ToList();

                Assert.True(response.IsSuccessStatusCode);
                Assert.Empty(produtosDb);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
            finally
            {
                baseConfig.Dispose();
            }
        }

        private IEnumerable<Produto> ObtemListaDeProdutos()
        {
            return new[]
            {
               new Produto()
                {
                    DescricaoProduto = "Produto de Teste 1",
                    SituacaoProduto = true,
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(30),
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Fornecedor de Teste 1",
                    CnpjFornecedor = "12345678901234"
                },
               new Produto()
                {
                    DescricaoProduto = "Produto de Teste 2",
                    SituacaoProduto = true,
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(30),
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Fornecedor de Teste 2",
                    CnpjFornecedor = "12345678901234"
                },
                new Produto()
                {
                    DescricaoProduto = "Produto de Teste 3",
                    SituacaoProduto = true,
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(30),
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Fornecedor de Teste 3",
                    CnpjFornecedor = "12345678901234"
                },
                new Produto()
                {
                    DescricaoProduto = "Produto de Teste 4",
                    SituacaoProduto = true,
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(30),
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Fornecedor de Teste 4",
                    CnpjFornecedor = "12345678901234"
                },
                 new Produto()
                {
                    DescricaoProduto = "Outro Produto",
                    SituacaoProduto = true,
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(30),
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Outro Fornecedor",
                    CnpjFornecedor = "12345678901234"
                },
                new Produto()
                {
                    DescricaoProduto = "Outro Produto 1",
                    SituacaoProduto = true,
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(30),
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Fornecedor de Teste 2",
                    CnpjFornecedor = "12345678901234"
                },
                new Produto()
                {
                    DescricaoProduto = "Outro Produto 2",
                    SituacaoProduto = true,
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(30),
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Fornecedor de Teste 2",
                    CnpjFornecedor = "12345678901234"
                },
                new Produto()
                {
                    DescricaoProduto = "Produto de Teste",
                    SituacaoProduto = true,
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(30),
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Fornecedor de Teste",
                    CnpjFornecedor = "12345678901234"
                },
                new Produto()
                {
                    DescricaoProduto = "Produto de Teste",
                    SituacaoProduto = true,
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(30),
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Fornecedor de Teste",
                    CnpjFornecedor = "12345678901234"
                },
                new Produto()
                {
                    DescricaoProduto = "Produto de Teste",
                    SituacaoProduto = true,
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(30),
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Fornecedor de Teste",
                    CnpjFornecedor = "12345678901234"
                },
            }.ToList();
        }
    }
}
