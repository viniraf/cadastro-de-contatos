using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroContato.Models;
using CadastroContato.Data;
using CadastroContato.Repositorio;

namespace Cadastrousuario.Repositorio {
    public class UsuarioRepositorio : IUsuarioRepositorio {

        private readonly BancoContext _context;

        public UsuarioRepositorio(BancoContext bancoContext) {

            _context = bancoContext;
        }

        public UsuarioModel Create(UsuarioModel usuario) {

            usuario.DataCadastro = DateTime.Now;
            usuario.SetPasswordHash();
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public List<UsuarioModel> GetAll() {

            return _context.Usuarios.ToList();
        }

        public UsuarioModel GetUserByLogin(string login) {
            return _context.Usuarios.FirstOrDefault(x => x.Login == login);
        }

        public UsuarioModel GetInfosById(int id) {

            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioModel Update(UsuarioModel usuario) {

            UsuarioModel usuarioDb = GetInfosById(usuario.Id);

            if (usuarioDb == null) throw new Exception("Houve um erro na atualização do usuario: consulta retornando sem dados");

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Login = usuario.Login;
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.DataAtualizacao = DateTime.Now;

            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();

            return usuarioDb;
        }

        public bool Delete(int id) {

            UsuarioModel usuarioDb = GetInfosById(id);

            if (usuarioDb == null) throw new Exception("Houve um erro na deleção do usuario: consulta retornando sem dados");

            _context.Usuarios.Remove(usuarioDb);
            _context.SaveChanges();

            return true;
        }

        
    }
}
