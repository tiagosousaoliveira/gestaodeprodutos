using System;

namespace GestaoDeProdutos.Inputs
{
    public sealed record AtualizarProdutoInput(
        int CodigoProduto,
        string DescricaoProduto, 
        bool SituacaoProduto, 
        DateTime DataFabricacao, 
        DateTime DataValidade, 
        int CodigoFornecedor, 
        string DescricaoFornecedor,
        string CnpjFornecedor);
}
