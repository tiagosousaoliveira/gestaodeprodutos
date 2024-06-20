using System;

namespace Aplication.Dto
{
    public sealed record ProdutoDto(
        int CodigoProduto,
        string DescricaoProduto,
        bool SituacaoProduto,
        DateTime DataFabricacao,
        DateTime DataValidade,
        int CodigoFornecedor,
        string DescricaoFornecedor,
        string CnpjFornecedor);
}

