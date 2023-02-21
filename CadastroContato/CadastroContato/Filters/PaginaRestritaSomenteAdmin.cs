﻿using CadastroContato.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContato.Filters {
    public class PaginaRestritaSomenteAdmin : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext context) {

            string sessaoUsuario = context.HttpContext.Session.GetString("UsuarioLogado");

            if(string.IsNullOrEmpty(sessaoUsuario)) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } } );
            }

            else {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

                if(usuario == null) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                if (usuario.Perfil != Enums.PerfilEnum.Admin) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
