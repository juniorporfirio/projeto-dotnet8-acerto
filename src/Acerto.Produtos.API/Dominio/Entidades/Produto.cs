using System.ComponentModel.DataAnnotations;

namespace Acerto.Produtos.API.Dominio.Entidades;
public record Produto(
    [Required(ErrorMessage ="Id é obrigatório")] Guid Id,
    [Required(ErrorMessage ="Nome é obrigatório")] string Nome, 
    string Descricao, 
    [Range(1, int.MaxValue, ErrorMessage ="Preco deve ser maior que zero")] decimal Preco, 
    bool Ativo);
