using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Data;
using CadastroContato.Models;

namespace CadastroContato.Repositorio {
    public class ContatoRepositorio : IContatoRepositorio {

        private readonly BancoContext _bancoContext;

        public ContatoRepositorio(BancoContext bancoContext) {
            _bancoContext = bancoContext;
        }

        public ContatoModel Create(ContatoModel contato) {

            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public List<ContatoModel> GetAll() {
            return _bancoContext.Contatos.ToList();
        }
    }
}
