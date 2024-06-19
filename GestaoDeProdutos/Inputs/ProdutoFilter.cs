using System;

namespace GestaoDeProdutos.Inputs
{
    public sealed record ProdutoFilter(
        int? CodigoProduto,
        string DescricaoProduto,
        bool SituacaoProduto,
        DateTime DataFabricacao,
        DateTime DataValidade,
        int CodigoFornecedor,
        string DescricaoFornecedor,
        string CnpjFornecedor,
        int? Pagina = 1,
        int? QuantidadePorPagina = 8);

}
