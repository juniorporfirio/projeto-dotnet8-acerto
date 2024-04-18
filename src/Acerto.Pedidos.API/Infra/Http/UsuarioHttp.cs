using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acerto.Pedidos.API.Infra.Http
{
    public class UsuarioHttp
    {
        public string NomeUsuario { get; set; } = "";
        public string Senha { get; set; } = string.Empty;
        public string Papel { get; set; } = string.Empty;
    }
}