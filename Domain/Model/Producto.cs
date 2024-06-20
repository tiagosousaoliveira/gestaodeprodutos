using Domain.Model;
using System;

namespace Domain.Model
{
    public class Produto
    {
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public bool SituacaoProduto { get; set; } 
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CnpjFornecedor { get; set; }

        public Produto()
        {
            
        }

        public Produto(int codigo)
        {
            this.CodigoProduto = codigo;
        }
    }
}