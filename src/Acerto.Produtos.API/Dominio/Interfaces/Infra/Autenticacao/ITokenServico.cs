using Acerto.Produtos.API.Dominio.Entidades;

namespace Acerto.Produtos.API.Dominio.Interfaces.Infra.Autenticacao
{
    public interface ITokenServico
    {
        string GerarToken(Usuario usuario);
    }
}