using Acerto.Pedidos.API.Apresentacao;
using Acerto.Pedidos.API.Apresentacao.Endpoints.Validacao;
using Acerto.Pedidos.API.Dominio.Entidades;
using Acerto.Pedidos.API.Dominio.Interfaces.Fila;
using Acerto.Pedidos.API.Dominio.Interfaces.Repositorio;
using Acerto.Pedidos.API.Infra.Http;
using Acerto.Pedidos.API.Infra.Http.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace Acerto.Pedidos.API.UnitTests.Apresentacao.Endpoints
{
    public class PedidoEndpointTest
    {
        private readonly Mock<IProdutoRest> _produtoRestMock;
        private readonly Mock<IPedidoRepositorio> _pedidoRepositorioMock;
        private readonly Mock<IFilaIntegracao> _filaIntegracaoMock;

        public PedidoEndpointTest()
        {
            _produtoRestMock = new Mock<IProdutoRest>();
            _pedidoRepositorioMock = new Mock<IPedidoRepositorio>();
            _filaIntegracaoMock = new Mock<IFilaIntegracao>();
        }
        [Fact]
        public async Task Deve_adicionar_um_novo_pedido()
        {   
            var produtoHttp = new ProdutoHttp(Guid.NewGuid(), "Caneta Bic", "", 5,true);
            var validator = new PedidoValidator();
            var pedido = new Pedido{
                Id = Guid.NewGuid(),
                DataCriacao = DateTime.Now,
                Status = Dominio.Enum.PedidoStatus.Novo,
                Produtos = [ 
                    new(){ Id = produtoHttp.Id, Quantidade = 10}
                 ],    
            };

            _pedidoRepositorioMock.Setup(x=>x.Novo(It.IsAny<Pedido>())).Verifiable();
            _filaIntegracaoMock.Setup(x=>x.EnviarMensagem(It.IsAny<Pedido>())).Verifiable();

            _produtoRestMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(produtoHttp);

            var novo = await PedidoEndpoint.Novo(pedido,validator,_produtoRestMock.Object,_pedidoRepositorioMock.Object,_filaIntegracaoMock.Object);

            novo.Result.Should().NotBeNull();
            novo.Result.Should().BeOfType<Created<Pedido>>();
            
            var item = (Created<Pedido>)novo.Result;

            item.Value!.DataCriacao.Should().Be(pedido.DataCriacao);
            item.Value.Id.Should().Be(pedido.Id);
            item.Value.Status.Should().Be(pedido.Status);
            item.Value.ValorTotal.Should().Be(50);

            var primeiroProduto = item.Value.Produtos.First();
            primeiroProduto.Id.Should().Be(produtoHttp.Id);
            primeiroProduto.Nome.Should().Be(produtoHttp.Nome);
            primeiroProduto.Preco.Should().Be(produtoHttp.Preco);

            _produtoRestMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once());
            _pedidoRepositorioMock.Verify(x => x.Novo(It.IsAny<Pedido>()), Times.Once());
            _filaIntegracaoMock.Verify(x => x.EnviarMensagem(It.IsAny<Pedido>()), Times.Once());
        }
    }
}