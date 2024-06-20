using AutoMapper;
using Domain.Dto;
using Domain.Model;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public sealed class ProdutoRepository : Repository, IProdutoRepository
    {
        private readonly IMapper _mapper;
        public ProdutoRepository(DatabaseContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public Task AtualizarAsync(Produto produto)
        {
            var entry = Context.Entry(produto);
            entry.State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task ExcluirAsync(int codigoProduto)
        {
            var produtoDb = Context.Produtos
                .AsNoTracking()
                .First(x => x.CodigoProduto == codigoProduto);

            produtoDb.SituacaoProduto = false;
            Context.Produtos.Update(produtoDb);
            return Task.CompletedTask;
        }

        public Task InserirAsync(Produto produto)
            => Context.AddRangeAsync(produto);

        public async Task<PaginacaoDto<Produto>> ObterListaArync(ParametrosDto parametros)
        {
            var query = Context.Produtos.AsQueryable();
            var totalRegistros = await query.CountAsync();

            if (!String.IsNullOrWhiteSpace(parametros.DescricaoProduto))
                query = query.Where(x => x.DescricaoProduto == parametros.DescricaoProduto);
            if (!parametros.CodigoFornecedor.HasValue)
                query = query.Where(x => x.CodigoFornecedor == parametros.CodigoFornecedor);
            if (!String.IsNullOrWhiteSpace(parametros.DescricaoFornecedor))
                query = query.Where(x => x.DescricaoFornecedor == parametros.DescricaoFornecedor);
            if (!String.IsNullOrWhiteSpace(parametros.CnpjFornecedor))
                query = query.Where(x => x.CnpjFornecedor == parametros.CnpjFornecedor);

            var totalPages = (int)Math.Ceiling(totalRegistros / (double)parametros.QuantidadePorPagina.Value);
            var paginatedQuery = query.Skip((parametros.Pagina.Value - 1) * parametros.QuantidadePorPagina.Value)
                                    .Take(parametros.QuantidadePorPagina.Value);

            var data = await paginatedQuery.ToListAsync();
            return new PaginacaoDto<Produto>
            {
                PageNumber = parametros.Pagina.Value,
                PageSize = parametros.QuantidadePorPagina.Value,
                TotalPages = totalPages,
                TotalRecords = totalRegistros,
                Data = data
            };
        }

        public Task<Produto> ObterPorCodigoAsync(int codigo)
            => Context.Produtos
                .AsNoTracking()
                .Where(x => x.CodigoProduto == codigo).FirstAsync();
    }
}
