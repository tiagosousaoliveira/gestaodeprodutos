using System;

namespace Domain.Exceptions
{
    public sealed class ObterListaDeProdutoException : Exception
    {
        private const string mensagem = "Ocorreu um erro ao tentar ibter a lista de Produtos";
        public ObterListaDeProdutoException() 
            : base(mensagem)
        {

        }
    }
}
