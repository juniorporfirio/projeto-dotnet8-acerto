namespace Acerto.Produtos.API.Dominio.Entidades;

public class Usuario
{
    public string NomeUsuario { get; set; } = "";
    public string Senha { get; set; } = string.Empty;
    public string Papel { get; set; } = string.Empty;
}