using Acerto.Produtos.API.Dominio.Entidades;

namespace Acerto.Produtos.API.Dominio.Interfaces.Infra.Repositorio;

public interface IProdutosRepository
{
     public Task<Produto?> PorId(Guid Id);
    public Task<IEnumerable<Produto>> Todos();
    public Task AdicionarNovo(Produto produto);
     public Task Atualizar(Produto produto);
    public Task Remove(Guid Id);
}