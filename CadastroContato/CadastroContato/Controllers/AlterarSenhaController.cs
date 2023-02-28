using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Helper;
using CadastroContato.Models;
using CadastroContato.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContato.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao) {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePassword(AlterarSenhaModel alterarSenhaModel) {

            try {

                if(ModelState.IsValid) {

                    UsuarioModel usuarioLogado = _sessao.GetUserSession();
                    alterarSenhaModel.Id = usuarioLogado.Id;

                    _usuarioRepositorio.UpdatePassword(alterarSenhaModel);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return View("Index", alterarSenhaModel);
                }

                return View("Index", alterarSenhaModel);

            }
            catch (Exception error) {

                TempData["MensagemErro"] = $"Ops, não foi possível alterar sua senha, tente novamente! Detalhe do erro: {error.Message}";
                return View("Index", alterarSenhaModel);
            }

        }
    }
}