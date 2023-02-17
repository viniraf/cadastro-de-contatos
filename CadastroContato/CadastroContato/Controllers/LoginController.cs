using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Models;
using CadastroContato.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContato.Controllers {
    public class LoginController : Controller {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController (IUsuarioRepositorio usuarioRepositorio) {
            _usuarioRepositorio = usuarioRepositorio;
        }



        public IActionResult Index() {

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel) {

            try {

                if (ModelState.IsValid) {

                   UsuarioModel usuario = _usuarioRepositorio.GetUserByLogin(loginModel.Login);

                    if (usuario != null) {

                        if (usuario.SenhaValida(loginModel.Senha)) {

                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha do usuário é inválida! Tente novamente!";
                    }

                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente!";
                }

                return View("Index");

            }
            catch (Exception error) {

                TempData["MensagemErro"] = $"Ops, não foi possível realizar seu login, tente novamente! Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }


    }
}