using Aplication.Commands;
using Aplication.Dto;
using AutoMapper;
using Domain.Model;

namespace Aplication.Mappings
{
    public class AtualizarProdutoProfile : Profile
    {
        public AtualizarProdutoProfile()
        {
            CreateMap<AtualizarProdutoCommand.Produto, Produto>();
        }
    }
}
