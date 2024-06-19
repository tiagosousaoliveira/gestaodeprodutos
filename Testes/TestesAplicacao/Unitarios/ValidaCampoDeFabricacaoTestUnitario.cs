using Aplication.Commands;
using Domain.Model;
using Domain.Persistence;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Testes.TestesAplicacao.Unitarios
{
    public class ValidaCampoDeFabricacaoTestUnitario
    {
        private Mock<IProdutoRepository> _produtoRepositoryMock {  get; set; }
        private Mock<InserirProdutoCommand> _commandMock { get; set; }
        

        public ValidaCampoDeFabricacaoTestUnitario()
        {
            this._produtoRepositoryMock = new Mock<IProdutoRepository>();
        }

        [Fact]
        public void Deve_Validar_Datas_Corretamente_Quando_Sao_Válidas()
        {
            // Arrange
            var command = new InserirProdutoCommand(new InserirProdutoCommand.Produto(
                DescricaoProduto: "Teste",
                SituacaoProduto: true,
                DataFabricacao: new DateTime(2024, 6, 15),
                DataValidade: new DateTime(2024, 7, 10),
                CodigoFornecedor: 1,
                DescricaoFornecedor: "Fornecedor Teste",
                CnpjFornecedor: "12345678901234"
            ));

            // Act
            var validator = new ProdutoValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should();
        }

        [Fact]
        public void Deve_Validar_DataFabricacao_Menor_ou_Igual_a_DataValidade()
        {
            // Arrange
            var command = new InserirProdutoCommand(new InserirProdutoCommand.Produto(
                DescricaoProduto: "Teste",
                SituacaoProduto: true,
                DataFabricacao: new DateTime(2024,7,20),
                DataValidade: new DateTime(2024, 7, 20),
                CodigoFornecedor: 1,
                DescricaoFornecedor: "Fornecedor Teste",
                CnpjFornecedor: "12345678901234"
            ));

            // Act
            var validator = new ProdutoValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(error => error.ErrorMessage == "A data de fabricação não pode ser maior ou igual à data de validade.");
        }
    }
}
