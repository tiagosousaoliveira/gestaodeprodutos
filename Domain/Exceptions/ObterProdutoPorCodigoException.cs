using System;

namespace Domain.Exceptions
{
    public sealed class ObterProdutoPorCodigoException : Exception
    {
        private const string mensagem = "Ocorreu um erro ao tentar obter o Produto";
        public ObterProdutoPorCodigoException() 
            : base(mensagem)
        {

        }
    }
}
