using Refit;

namespace Acerto.Pedidos.API.Infra.Http.Interfaces
{
    public interface IAutenticacaoRest
    {
        [Post("/autenticacao/login")]
        Task<TokenHttp> Autenticar([Body] UsuarioHttp usuario);
    }
}