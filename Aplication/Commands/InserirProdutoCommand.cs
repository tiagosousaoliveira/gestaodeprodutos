using AutoMapper;
using Domain.Exceptions;
using Domain.Model;
using Domain.Persistence;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Commands
{
    public sealed class InserirProdutoCommandHandler : IRequestHandler<InserirProdutoCommand, InserirProdutoCommand.Response>
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;
        public InserirProdutoCommandHandler(IProdutoRepository repository,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<InserirProdutoCommand.Response> Handle(InserirProdutoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.InserirAsync(_mapper.Map<Produto>(request.ProdutoInput));
                await _repository.CommitAsync();
                return new InserirProdutoCommand.Response();

            }catch(Exception)
            {
                throw new InserirProdutoException();
            }
        }
    }
    public sealed record InserirProdutoCommand(InserirProdutoCommand.Produto ProdutoInput) : IRequest<InserirProdutoCommand.Response>
    {
        public sealed record Produto(
            string DescricaoProduto,
            bool SituacaoProduto,
            DateTime DataFabricacao,
            DateTime DataValidade,
            int CodigoFornecedor,
            string DescricaoFornecedor,
            string CnpjFornecedor);
        public sealed record Response();
    }

    public class ProdutoValidator : AbstractValidator<InserirProdutoCommand>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.ProdutoInput.DataFabricacao).LessThan(x => x.ProdutoInput.DataValidade).WithMessage("A data de fabricação não pode ser maior ou igual à data de validade.");
        }
    }
}
