using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Helper;
using CadastroContato.Models;
using CadastroContato.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContato.Controllers {
    public class LoginController : Controller {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController (IUsuarioRepositorio usuarioRepositorio, ISessao sessao) {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index() {
            // Se usuario estiver logado, redireciona para home

            if (_sessao.GetUserSession() != null) {

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Exit() {

            _sessao.RemoveUserSession();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel) {

            try {

                if (ModelState.IsValid) {

                   UsuarioModel usuario = _usuarioRepositorio.GetUserByLogin(loginModel.Login);

                    if (usuario != null) {

                        if (usuario.SenhaValida(loginModel.Senha)) {

                            _sessao.CreateUserSession(usuario);
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