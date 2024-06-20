using Domain.Dto;
using Domain.Model;
using System.Threading.Tasks;

namespace Domain.Persistence
{
    public interface IProdutoRepository : IRepository
    {
        Task<Produto> ObterPorCodigoAsync(int codigo);
        Task<PaginacaoDto<Produto>> ObterListaArync(ParametrosDto parametros);
        Task InserirAsync(Produto Produto);
        Task AtualizarAsync(Produto Produto);
        Task ExcluirAsync(int CodigoProduto);

    }
}
