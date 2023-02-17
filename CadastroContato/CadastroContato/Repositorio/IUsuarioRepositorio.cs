using CadastroContato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContato.Repositorio {
    public interface IUsuarioRepositorio {

        UsuarioModel Create(UsuarioModel contato);

        List<UsuarioModel> GetAll();

        UsuarioModel GetUserByLogin(string login);

        UsuarioModel GetInfosById(int id);

        UsuarioModel Update(UsuarioModel contato);

        bool Delete(int id);
    }
}
