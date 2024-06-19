using Aplication.Commands;
using Aplication.Queries;
using AutoMapper;
using Domain.Dto;
using GestaoDeProdutos.Inputs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestaoDeProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProdutosController(IMediator mediator,
            IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ProdutoFilter parametros)
        {
            var retorno = await _mediator.Send(new ObterListaDeProdutosQuery(_mapper.Map<ParametrosDto>(parametros)));
            return Ok(retorno.lsitaDeProdutos);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ObterPorCodigo(int codigo)
        {
            var retorno = await _mediator.Send(new ObterProdutoPorCodigoQuery(codigo));
            return Ok(retorno.produto);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir(IncluirProdutoInput input)
        {
            var retorno = await _mediator.Send(new InserirProdutoCommand(_mapper.Map<InserirProdutoCommand.Produto>(input)));
            return Ok(retorno);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarProdutoInput input)
        {
            var retorno = await _mediator.Send(new AtualizarProdutoCommand(_mapper.Map<AtualizarProdutoCommand.Produto>(input)));
            return Ok(retorno);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Excluir(int codigo)
        {
            var retorno = await _mediator.Send(new ExcluirProdutoCommand(codigo));
            return Ok(retorno);
        }
    }
}
