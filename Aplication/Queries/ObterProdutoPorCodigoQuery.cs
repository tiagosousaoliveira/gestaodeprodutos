using Aplication.Dto;
using AutoMapper;
using Domain.Exceptions;
using Domain.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Queries
{
    public sealed class ObterProdutoPorCodigoQueryHandler : IRequestHandler<ObterProdutoPorCodigoQuery, ObterProdutoPorCodigoQuery.Response>
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ObterProdutoPorCodigoQueryHandler(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ObterProdutoPorCodigoQuery.Response> Handle(ObterProdutoPorCodigoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var produto = await _repository.ObterPorCodigoAsync(request.Codigo);
                return new ObterProdutoPorCodigoQuery.Response(_mapper.Map<ProdutoDto>(produto));

            }catch(Exception)
            {
                throw new ObterProdutoPorCodigoException();
            }
        }
    }
    public sealed record ObterProdutoPorCodigoQuery(int Codigo) : IRequest<ObterProdutoPorCodigoQuery.Response>
    {
        public sealed record Response(ProdutoDto produto);
    }
}
