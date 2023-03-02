using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Data;
using CadastroContato.Models;

namespace CadastroContato.Repositorio {
    public class ContatoRepositorio : IContatoRepositorio {

        private readonly BancoContext _context;

        public ContatoRepositorio(BancoContext bancoContext) {

            _context = bancoContext;
        }

        public ContatoModel Create(ContatoModel contato) {

            _context.Contatos.Add(contato);
            _context.SaveChanges();
            return contato;
        }

        public List<ContatoModel> GetAll(int usuarioId) {

            return _context.Contatos.Where(x => x.UsuarioId == usuarioId).ToList();
        }

        public ContatoModel GetInfosById(int id) {

            return _context.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public ContatoModel Update(ContatoModel contato) {

            ContatoModel contatoDb = GetInfosById(contato.Id);

            if (contatoDb == null) throw new Exception("Houve um erro na atualização do contato: consulta retornando sem dados");

            contatoDb.Nome = contato.Nome;
            contatoDb.Email = contato.Email;
            contatoDb.Celular = contato.Celular;

            _context.Contatos.Update(contatoDb);
            _context.SaveChanges();

            return contatoDb;
        }

        public bool Delete(int id) {

            ContatoModel contatoDb = GetInfosById(id);

            if (contatoDb == null) throw new Exception("Houve um erro na atualização do contato: consulta retornando sem dados");

            _context.Contatos.Remove(contatoDb);
            _context.SaveChanges();

            return true;
        }
    }
}
