using Domain.Exceptions;
using Domain.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Commands
{
    public sealed class ExcluirProdutoCommandHandler : IRequestHandler<ExcluirProdutoCommand, ExcluirProdutoCommand.Response>
    {
        private readonly IProdutoRepository _repository;

        public ExcluirProdutoCommandHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ExcluirProdutoCommand.Response> Handle(ExcluirProdutoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.ExcluirAsync(request.CodigoProduto);
                await _repository.CommitAsync();
                return new ExcluirProdutoCommand.Response();
            }
            catch (Exception)
            {
                throw new ExcluirProdutoException();
            }

            throw new System.NotImplementedException();
        }
    }
    public sealed record ExcluirProdutoCommand(int CodigoProduto) : IRequest<ExcluirProdutoCommand.Response>
    {
        public sealed record Response();
    }
}
