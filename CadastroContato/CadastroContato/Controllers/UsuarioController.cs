using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Models;
using CadastroContato.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContato.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio) {

            _usuarioRepositorio = usuarioRepositorio;
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

    }
}