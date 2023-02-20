using CadastroContato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContato.Helper {
    public interface ISessao {

        void CreateUserSession(UsuarioModel usuario);

        void RemoveUserSession();

        UsuarioModel GetUserSession();
    }
}
