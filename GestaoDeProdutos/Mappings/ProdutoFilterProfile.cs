using AutoMapper;
using Domain.Dto;
using GestaoDeProdutos.Inputs;

namespace GestaoDeProdutos.Mappings
{
    public class ProdutoFilterProfile : Profile
    {
        public ProdutoFilterProfile() 
        {
            CreateMap<ProdutoFilter, ParametrosDto>();
        }
    }
}
