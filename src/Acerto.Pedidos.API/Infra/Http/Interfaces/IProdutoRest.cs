using Refit;

namespace Acerto.Pedidos.API.Infra.Http.Interfaces
{
    [Headers("Authorization:Bearer")]
    public interface IProdutoRest
    {
        [Get("/produtos/{id}")]
        Task<ProdutoHttp> Get(Guid id);
    }
}