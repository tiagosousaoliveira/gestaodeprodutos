using Aplication.Commands;
using AutoMapper;
using Domain.Model;

namespace Aplication.Mappings
{
    public class IncluirProdutoProfile : Profile
    {
        public IncluirProdutoProfile()
        {
            CreateMap<InserirProdutoCommand.Produto, Produto>();
        }
    }
}
