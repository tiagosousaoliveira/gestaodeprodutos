using Aplication.Dto;
using AutoMapper;
using Domain.Dto;
using Domain.Model;

namespace Aplication.Mappings
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoDto>();
            CreateMap<PaginacaoDto<Produto>, PaginacaoDto<ProdutoDto>>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
