using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Models;
using CadastroContato.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContato.Controllers {
    public class ContatoController : Controller {

        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio) {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index() {

            List<ContatoModel> contatos = _contatoRepositorio.GetAll();

            return View(contatos);
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContatoModel contato) {
            _contatoRepositorio.Create(contato);
            return RedirectToAction("Index");
        }


        public IActionResult Update() {
            return View();
        }

        public IActionResult DeleteConfirmation() {
            return View();
        }

    }
}