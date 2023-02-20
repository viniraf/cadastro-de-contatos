using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CadastroContato.Helper {
    public class Sessao : ISessao {

        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext) {

            _httpContext = httpContext;
        }

        public void CreateUserSession(UsuarioModel usuario) {

            string valor = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("UsuarioLogado", valor);
        }

        public UsuarioModel GetUserSession() {

            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("UsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void RemoveUserSession() {

            _httpContext.HttpContext.Session.Remove("UsuarioLogado");
        }
    }
}
