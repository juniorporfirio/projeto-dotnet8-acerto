using Acerto.Produtos.API.Dominio.Interfaces.Infra.Repositorio;
using Acerto.Produtos.API.Dominio.Entidades;
using Acerto.Produtos.API.Apresentacao.Endpoints;
using Acerto.Produtos.API.Apresentacao.Endpoints.Validation;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace Acerto.Produtos.API.UnitTests.Presentation.Endpoints;

public class ProdutoEndpointTest
{
    private Mock<IProdutosRepository> _repositoryMoq;
    public ProdutoEndpointTest() => _repositoryMoq = new Mock<IProdutosRepository>();
    [Fact]
    public async Task Deve_retornar_todos_os_produtos()
    {
        _repositoryMoq.Setup(s => s.Todos()).ReturnsAsync([]);

        var todos = await ProdutosEndpoint.Todos(_repositoryMoq.Object);

        todos.Value!.Count().Equals(0);

        todos.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

  [Fact]
    public async Task Deve_recuperar_um_produto_pelo_codigo()
    {
        var id = Guid.NewGuid();
        var produto = new Produto(id, "Bic", "", 2, true);

        _repositoryMoq.Setup(s => s.PorId(id)).ReturnsAsync(produto);

        var porID = await ProdutosEndpoint.PorId(id, _repositoryMoq.Object);

        porID.Result.Should().BeOfType<Ok<Produto>>();
    
        var dados = (Ok<Produto>) porID.Result;
        dados.Value.Should().NotBeNull();
        dados.Value!.Id.Should().Be(id);
        dados.Value!.Preco.Should().Be(produto.Preco);
        dados.Value!.Ativo.Should().Be(produto.Ativo);

        _repositoryMoq.Verify(s => s.PorId(id), Times.Once);

    }

     [Fact]
    public async Task Deve_validar_o_codigo_um_produto()
    {
        var id = Guid.NewGuid();

        _repositoryMoq.Setup(s => s.PorId(id));

        var porID = await ProdutosEndpoint.PorId(id, _repositoryMoq.Object);

        porID.Result.Should().BeOfType<NotFound>();
    
        _repositoryMoq.Verify(s => s.PorId(id), Times.Once);

    }
    [Fact]
    public async Task Deve_adicionar_um_novo_produto()
    {
        var novoProduto = new Produto(Guid.NewGuid(), "Caneta Bic", "", 2, true);
        var valid = new ProdutoValidator();

        var novo = await ProdutosEndpoint.Novo(novoProduto, valid, _repositoryMoq.Object);

        novo.Should().NotBeNull();

        Assert.IsType<Created<Produto>>(novo.Result);

    }

    [Fact]
    public async Task Deve_validar_o_nome_de_um_novo_produto()
    {
        var novoProduto = new Produto(Guid.NewGuid(), "", "", 2, true);
        var valid = new ProdutoValidator();

        var novo = await ProdutosEndpoint.Novo(novoProduto, valid, _repositoryMoq.Object);

        Assert.IsType<ValidationProblem>(novo.Result);

    }

    [Fact]
    public async Task Deve_atualizar_um_produto_existente()
    {
        var novoProduto = new Produto(Guid.NewGuid(), "Caneta Bic", "", 2, true);
        var valid = new ProdutoValidator();

        _repositoryMoq.Setup(s => s.PorId(It.IsAny<Guid>())).ReturnsAsync(novoProduto);
        _repositoryMoq.Setup(s => s.Atualizar(It.IsAny<Produto>())).Verifiable();

        var atualizar = await ProdutosEndpoint.Atualizar(novoProduto, valid, _repositoryMoq.Object);

        Assert.IsType<Created<Produto>>(atualizar.Result);

        _repositoryMoq.Verify(s => s.PorId(It.IsAny<Guid>()), Times.Once);
        _repositoryMoq.Verify(s => s.Atualizar(It.IsAny<Produto>()), Times.Once);
    }

    [Fact]
    public async Task Deve_retornar_nao_encontrado_ao_atualizar_um_produto_nao_existente()
    {
        var novoProduto = new Produto(Guid.NewGuid(), "Caneta Bic", "", 2, true);
        var valid = new ProdutoValidator();

        _repositoryMoq.Setup(s => s.PorId(It.IsAny<Guid>()));
        _repositoryMoq.Setup(s => s.Atualizar(It.IsAny<Produto>())).Verifiable();

        var atualizar = await ProdutosEndpoint.Atualizar(novoProduto, valid, _repositoryMoq.Object);

        Assert.IsType<NotFound>(atualizar.Result);

        _repositoryMoq.Verify(s => s.PorId(It.IsAny<Guid>()), Times.Once);
        _repositoryMoq.Verify(s => s.Atualizar(It.IsAny<Produto>()), Times.Never);
    }

    [Fact]
    public async Task Deve_validar_ao_atualizar_um_produto_sem_nome()
    {
        var novoProduto = new Produto(Guid.NewGuid(), "", "", 2, true);
        var valid = new ProdutoValidator();

        _repositoryMoq.Setup(s => s.PorId(It.IsAny<Guid>()));
        _repositoryMoq.Setup(s => s.Atualizar(It.IsAny<Produto>())).Verifiable();

        var atualizar = await ProdutosEndpoint.Atualizar(novoProduto, valid, _repositoryMoq.Object);

        Assert.IsType<ValidationProblem>(atualizar.Result);

        _repositoryMoq.Verify(s => s.PorId(It.IsAny<Guid>()), Times.Never);
        _repositoryMoq.Verify(s => s.Atualizar(It.IsAny<Produto>()), Times.Never);
    }

    [Fact]
    public async Task Deve_remover_um_produto()
    {
        var id = Guid.NewGuid();
        var produto = new Produto(id, "Bic", "", 2, true);
        _repositoryMoq.Setup(s => s.PorId(id)).ReturnsAsync(produto);
        _repositoryMoq.Setup(s => s.Remove(id)).Verifiable();

        var remover = await ProdutosEndpoint.Remover(id, _repositoryMoq.Object);

        remover.Result.Should().BeOfType<NoContent>();
        _repositoryMoq.Verify(s => s.PorId(id), Times.Once);
        _repositoryMoq.Verify(s => s.Remove(id), Times.Once);

    }

     [Fact]
    public async Task Deve_validar_o_codigo_ao_remover_um_produto()
    {
        var id = Guid.NewGuid();
        var produto = new Produto(id, "Bic", "", 2, true);
        _repositoryMoq.Setup(s => s.PorId(id));
        _repositoryMoq.Setup(s => s.Remove(id)).Verifiable();

        var remover = await ProdutosEndpoint.Remover(id, _repositoryMoq.Object);

        remover.Result.Should().BeOfType<NotFound>();
        _repositoryMoq.Verify(s => s.PorId(id), Times.Once);
        _repositoryMoq.Verify(s => s.Remove(id), Times.Never);

    }
}