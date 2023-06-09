﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Filters;
using CadastroContato.Helper;
using CadastroContato.Models;
using CadastroContato.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContato.Controllers {

    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller {

        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;


        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao) {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index() {

            UsuarioModel usuarioLogado = _sessao.GetUserSession();
            List<ContatoModel> contatos = _contatoRepositorio.GetAll(usuarioLogado.Id);

            return View(contatos);
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContatoModel contato) {

            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.GetUserSession();
                    contato.UsuarioId = usuarioLogado.Id;

                    contato =  _contatoRepositorio.Create(contato);

                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }


                return View(contato);
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível cadastrar seu contato, tente novamente! Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
            
        }


        public IActionResult Update(int id) {
            ContatoModel contato = _contatoRepositorio.GetInfosById(id);

            return View(contato);
        }

        [HttpPost]
        public IActionResult Update(ContatoModel contato) {

            try {
                if (ModelState.IsValid) {

                    UsuarioModel usuarioLogado = _sessao.GetUserSession();
                    contato.UsuarioId = usuarioLogado.Id;

                    contato = _contatoRepositorio.Update(contato);
                    TempData["MensagemSucesso"] = "Contato atualizado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contato);

            } catch (Exception error) {

                TempData["MensagemErro"] = $"Ops, não foi possível atualizar seu contato, tente novamente! Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }

        
        }

        public IActionResult DeleteConfirmation(int id) {
            ContatoModel contato = _contatoRepositorio.GetInfosById(id);
            return View(contato);
        }

        public IActionResult Delete(int id) {

            try {
                bool deleted =  _contatoRepositorio.Delete(id);

                if (deleted) {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                }  else {
                    TempData["MensagemErro"] = "Ops, não foi possível apagar seu contato";
                }
                return RedirectToAction("Index");
            } catch (Exception error) {

                TempData["MensagemErro"] = $"Ops, não foi possível apagar seu contato, tente novamente! Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}