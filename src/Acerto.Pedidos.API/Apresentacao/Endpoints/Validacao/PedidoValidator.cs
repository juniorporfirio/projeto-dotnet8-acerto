using Acerto.Pedidos.API.Dominio.Entidades;
using FluentValidation;

namespace Acerto.Pedidos.API.Apresentacao.Endpoints.Validacao
{
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            //RuleFor(r => r.Quantidade).GreaterThan(0).WithMessage("A quantidade deve ser maior que zero");
           // RuleFor(r => r.Produtos).Must(m => m.Any(d => d.Id == Guid.Empty)).WithMessage("Deve possuir pelo menos 1 produto");
           // RuleFor(r => r.Produtos.Count()).GreaterThan(0).WithMessage("Deve possuir ao menos um produto");
        }
    }
}