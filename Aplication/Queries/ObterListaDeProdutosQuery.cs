using Aplication.Dto;
using AutoMapper;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Queries
{
    public sealed class ObterListaDeProdutosQueryHandler : IRequestHandler<ObterListaDeProdutosQuery, ObterListaDeProdutosQuery.Response>
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ObterListaDeProdutosQueryHandler(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ObterListaDeProdutosQuery.Response> Handle(ObterListaDeProdutosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var listaDeProdutos = await _repository.ObterListaArync(request.ParametrosInput);
                return new ObterListaDeProdutosQuery.Response(_mapper.Map<PaginacaoDto<ProdutoDto>>(listaDeProdutos));
            }
            catch (Exception)
            {
                throw new ObterProdutoPorCodigoException();
            }
        }
    }
    public sealed record ObterListaDeProdutosQuery(ParametrosDto ParametrosInput) : IRequest<ObterListaDeProdutosQuery.Response>
    {
        public sealed record Response(PaginacaoDto<ProdutoDto> lsitaDeProdutos);
    }
}
