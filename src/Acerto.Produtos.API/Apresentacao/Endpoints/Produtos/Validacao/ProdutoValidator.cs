using Acerto.Produtos.API.Dominio.Entidades;
using FluentValidation;

namespace Acerto.Produtos.API.Apresentacao.Endpoints.Validation
{
    public class ProdutoValidator: AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p=>p.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório");

            RuleFor(p=>p.Id)
                .NotEmpty()
                .WithMessage("Id é obrigatório");
            
            RuleFor(p=>p.Preco)
            .GreaterThan(0)
            .WithMessage("Preço deve ser maior que zero");
        }
        
    }
}