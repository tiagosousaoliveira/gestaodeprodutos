using System;

namespace Domain.Exceptions
{
    public sealed class AtualizarProdutoException : Exception
    {
        private const string mensagem = "Ocorreu um erro ao Atualizar o Produto";
        public AtualizarProdutoException() 
            : base(mensagem)
        {

        }
    }
}
