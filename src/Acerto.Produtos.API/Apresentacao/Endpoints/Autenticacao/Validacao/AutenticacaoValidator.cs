using Acerto.Produtos.API.Dominio.Entidades;
using FluentValidation;

namespace Acerto.Produtos.API.Apresentacao.Endpoints.Autenticacao.Validacao
{
    public class AutenticacaoValidator: AbstractValidator<Usuario>
    {
        public AutenticacaoValidator()
        {
            RuleFor(r=>r.NomeUsuario).NotEmpty().WithMessage("Usuario obrigatório");
            RuleFor(r=>r.Senha).NotEmpty().WithMessage("Senha obrigatória");
        }
    }
}