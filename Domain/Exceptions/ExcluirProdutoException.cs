using System;

namespace Domain.Exceptions
{
    public sealed class ExcluirProdutoException : Exception
    {
        private const string mensagem = "Ocorreu um erro ao Excluir o Produto";
        public ExcluirProdutoException()
            : base(mensagem)
        {

        }
    }
}
