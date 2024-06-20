using AutoMapper;
using Domain.Exceptions;
using Domain.Model;
using Domain.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Commands
{
    public sealed class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand, AtualizarProdutoCommand.Response>
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public AtualizarProdutoCommandHandler(IProdutoRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<AtualizarProdutoCommand.Response> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.AtualizarAsync(_mapper.Map<Produto>(request.ProdutoInput));
                await _repository.CommitAsync();
                return new AtualizarProdutoCommand.Response();
            }
            catch (Exception)
            {
                throw new AtualizarProdutoException();
            }
        }
    }
    public sealed record AtualizarProdutoCommand(AtualizarProdutoCommand.Produto ProdutoInput) : IRequest<AtualizarProdutoCommand.Response>
    {
        public sealed record Produto(
            int CodigoProduto,
            string DescricaoProduto,
            bool SituacaoProduto,
            DateTime DataFabricacao,
            DateTime DataValidade,
            int CodigoFornecedor,
            string DescricaoFornecedor,
            string CnpjFornecedor);
        public sealed record Response();
    }

}
