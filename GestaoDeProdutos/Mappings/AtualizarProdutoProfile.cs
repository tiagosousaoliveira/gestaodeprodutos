using Aplication.Commands;
using AutoMapper;
using GestaoDeProdutos.Inputs;

namespace GestaoDeProdutos.Mappings
{
    public class AtualizarProdutoProfile : Profile
    {
        public AtualizarProdutoProfile()
        {
            CreateMap<AtualizarProdutoInput, AtualizarProdutoCommand.Produto>()
                 .ConstructUsing((from, ctx)
                    => new AtualizarProdutoCommand.Produto(from.CodigoProduto, from.DescricaoProduto, from.SituacaoProduto, from.DataFabricacao, from.DataValidade, from.CodigoFornecedor, from.DescricaoFornecedor, from.CnpjFornecedor));
        }
    }
}
