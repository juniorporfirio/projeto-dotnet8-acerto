

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Acerto.Produtos.API.Dominio.Configuracoes;
using Acerto.Produtos.API.Dominio.Entidades;
using Acerto.Produtos.API.Dominio.Interfaces.Infra.Autenticacao;
using Microsoft.IdentityModel.Tokens;

namespace Acerto.Produtos.API.Apresentacao.Endpoints.Autenticacao
{
    public class TokenServico: ITokenServico
    {
        public string GerarToken(Usuario usuario)
        {
             var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuracao.Chave);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, usuario.NomeUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Papel.ToString())
            ]),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}