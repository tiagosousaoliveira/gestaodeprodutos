using System;

namespace GestaoDeProdutos.Inputs
{
    public sealed record IncluirProdutoInput(
        string DescricaoProduto, 
        bool SituacaoProduto, 
        DateTime DataFabricacao, 
        DateTime DataValidade, 
        int CodigoFornecedor, 
        string DescricaoFornecedor,
        string CnpjFornecedor);
}
