using System;

namespace Domain.Dto
{
    public sealed record ParametrosDto(
        string? DescricaoProduto,
        int? CodigoFornecedor,
        string? DescricaoFornecedor,
        string? CnpjFornecedor,
        int? Pagina = 1,
        int? QuantidadePorPagina = 8);

}
