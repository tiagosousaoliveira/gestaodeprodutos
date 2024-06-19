using Aplication.Commands;
using AutoMapper;
using GestaoDeProdutos.Inputs;
using System;

namespace GestaoDeProdutos.Mappings
{
    public class IncluirProdutoProfile : Profile
    {
        public IncluirProdutoProfile()
        {
            CreateMap<IncluirProdutoInput, InserirProdutoCommand.Produto>()
                 .ConstructUsing((from, ctx) 
                    => new InserirProdutoCommand.Produto(from.DescricaoProduto, from.SituacaoProduto, from.DataFabricacao, from.DataValidade, from.CodigoFornecedor, from.DescricaoFornecedor, from.CnpjFornecedor));
        }
    }
}