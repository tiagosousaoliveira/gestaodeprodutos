using Domain.Model;
using Infra;
using System.Linq;
using System;
using Testes.Configuracao;
using Xunit;

namespace Testes.TestesAplicacao.Unitarios
{
    public class InsereProdutoemMemoriaTestsUnitario : DatabaseTestFixture
    {
        public InsereProdutoemMemoriaTestsUnitario(IntegrationWebFactory factory) 
            : base(factory)
        {
        }

        [Fact]
        public void Testes()
        {
            var request = NewRequest();
            var produto = new Produto
            {
                DescricaoProduto = "Produto de Teste",
                SituacaoProduto = true,
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(30),
                CodigoFornecedor = 1,
                DescricaoFornecedor = "Fornecedor de Teste",
                CnpjFornecedor = "12345678901234"
            };

            // Act
            using var context = request.CreateContextInmemory();
            context.Produtos.Add(produto);
            context.SaveChanges();

            // Assert
            var produtoSalvo = context.Produtos.FirstOrDefault(p => p.DescricaoProduto == produto.DescricaoProduto);
            Assert.NotNull(produtoSalvo);
            Assert.Equal(produto.DescricaoProduto, produtoSalvo.DescricaoProduto);

        }
    }
}
