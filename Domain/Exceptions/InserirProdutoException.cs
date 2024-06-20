using System;

namespace Domain.Exceptions
{
    public sealed class InserirProdutoException : Exception
    {
        private const string mensagem = "Ocorreu um erro ao inserir o Produto";
        public InserirProdutoException() 
            : base(mensagem)
        {

        }
    }
}
