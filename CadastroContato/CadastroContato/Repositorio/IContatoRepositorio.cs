using CadastroContato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContato.Repositorio {
    public interface IContatoRepositorio {

        ContatoModel Create(ContatoModel contato);

        List<ContatoModel> GetAll(int usuarioId);

        ContatoModel GetInfosById(int id);

        ContatoModel Update(ContatoModel contato);

        bool Delete(int id);
    }
}
