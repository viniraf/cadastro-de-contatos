using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Filters;
using CadastroContato.Models;
using CadastroContato.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContato.Controllers {

    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IContatoRepositorio _contatoRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IContatoRepositorio contatoRepositorio) {

            _usuarioRepositorio = usuarioRepositorio;
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index() {

            List<UsuarioModel> usuarios = _usuarioRepositorio.GetAll();
            return View(usuarios);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UsuarioModel usuario) {

            try {
                if (ModelState.IsValid) {

                    _usuarioRepositorio.Create(usuario);
                    TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }


                return View(usuario);
            }
            catch (Exception error) {

                TempData["MensagemErro"] = $"Ops, não foi possível cadastrar seu usuario, tente novamente! Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult GetContactsByUserId(int id) {

            List<ContatoModel> contatos = _contatoRepositorio.GetAll(id);
            return PartialView("_ContatosUsuario", contatos);
        }

        public IActionResult Update(int id) {

            UsuarioModel usuario = _usuarioRepositorio.GetInfosById(id);

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Update(UsuarioSemSenhaModel usuarioSemSenha) {

            try {
                UsuarioModel usuario = null;

                if (ModelState.IsValid) {

                    usuario = new UsuarioModel() {

                        Id = usuarioSemSenha.Id,
                        Nome = usuarioSemSenha.Nome,
                        Login = usuarioSemSenha.Login,
                        Email = usuarioSemSenha.Email,
                        Perfil = usuarioSemSenha.Perfil
                    };

                    usuario = _usuarioRepositorio.Update(usuario);
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception error) {

                TempData["MensagemErro"] = $"Ops, não foi possível atualizar seu usuário , tente novamente! Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }


        }

        public IActionResult DeleteConfirmation(int id) {

            UsuarioModel usuario = _usuarioRepositorio.GetInfosById(id);
            return View(usuario);
        }

        public IActionResult Delete(int id) {

            try {
                bool deleted = _usuarioRepositorio.Delete(id);

                if (deleted) {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso!";
                }
                else {
                    TempData["MensagemErro"] = "Ops, não foi possível apagar seu usuário";
                }

                return RedirectToAction("Index");
            }
            catch (Exception error) {

                TempData["MensagemErro"] = $"Ops, não foi possível apagar seu usuário, tente novamente! Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}