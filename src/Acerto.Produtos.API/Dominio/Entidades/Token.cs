namespace Acerto.Produtos.API.Dominio.Entidades
{
    public class BearerToken
    {
        public required string NomeUsuario { get; set; } 
        public required string Token { get; set; }
    }
}